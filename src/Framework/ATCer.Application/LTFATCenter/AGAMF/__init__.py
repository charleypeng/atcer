# -*-coding:UTF-8 -*-
#########################################################################
#  Copyright: ATMB-HN. All rights reserved.
#  Created By :Anle Pi@ 202.11
#  Tips:
#     下载管综飞行计划：http get
#           UrlFPFips =http://195.168.1.203:5000/api/fips/gettoday    (get)
#     上传数据至数据存储：
#           socket UDP port:
#               接收端IP:
#               IFPLTOMS = 40293
#               Cat048 = 40048
#               Cat062 = 40062
#	  至态势分析数据存储终端数据格式：
#			{ "TYPE":"类型",
#			  "DATA":{},
#             "MSG":"str"}  #类型关键字：C062,C048,IFPL,TOMS,TRACK9,METEO,ERROR
#     该版本中关于自观航标9数据的处理：暂考虑串口转UDP的传输，由于各客户端需要播报变化的阈值不同，故将筛选功能放在客户端
#
#
#########################################################################

import socketserver
import time
import datetime
import threading
import socket
import configparser
import json
import ctypes
import os
import binascii
import sys
import re
from icecream import ic
from AGAMF.Asterix import Asterix
from concurrent.futures import ThreadPoolExecutor,as_completed,wait
import urllib.parse
import urllib.request



class CFG:
	cf = configparser.ConfigParser()
	Current_dir = os.path.dirname(__file__)

	ConfigFilePath = os.path.join(Current_dir,'config.ini')
	#cf.read(r"F:\Data\PyCode\RemindClientOfTrack9\server\config.ini")  # 读取配置文件，如果写文件的绝对路径，就可以不用os模块
	cf.read(ConfigFilePath)
	Swtich_MeteoMsg = cf.get("Switch", "MeteoMsg").upper()
	Swtich_Track9 = cf.get("Switch", "TRACK9").upper()
	Switch_4029_3 = cf.get("Switch", "MHT4029_3").upper()
	Switch_FPFips = cf.get("Switch", "FPFips").upper()
	Switch_Cat048 = cf.get("Switch", "Cat048").upper()
	Switch_Cat062 = cf.get("Switch", "Cat062").upper()

	#数据接收端口
	Cat048UDPReceiverPort = int(cf.get("NetworkGet","Cat048UDPReceiverPort") )      #原始雷达数据UDP接收端口，cat001 cat048
	Cat062UDPReceiverPort = int(cf.get('NetworkGet','Cat062UDPReceiverPort'))  #综合雷达数据UDP接收端口 cat062
	IfplTomsUDPReceiverPort = int(cf.get('NetworkGet','IfplTomsUDPReceiverPort'))               #飞行计划同步信息UDP接收端口 MH/T-4029.3
	UrlGetFPFromFPFips = cf.get("NetworkGet", "UrlGetFPFromFPFips")
	FipsGetInterval = int(cf.get("NetworkGet", "FipsGetInterval"))
	MeteoMsgSerialCOM = cf.get('NetworkGet','MeteoMsgSerialCOM')                      #气象报文信息串口端口号
	MeteoMsgBaudrate = int(cf.get('NetworkGet','MeteoMsgBaudrate'))             #气象报文信息串口波特率
	MeteoAirport = []
	for i in cf.get('NetworkGet', 'MeteoAirport').split(','):         #METAR SPECI报筛选机场
		if i and not i.isspace():
			MeteoAirport.append(i)
	PatternGetTrack9 = cf.get('NetworkGet', 'PatternGetTrack9')
	Track9TcpIP = cf.get('NetworkGet','Track9TcpIP')
	Track9TcpPort = int(cf.get('NetworkGet','Track9TcpPort'))            #自观数据TCP接收端口
	Track9UdpPort = int(cf.get('NetworkGet', 'Track9UdpPort'))

	#数据发送端口
	UdpBroadcastIP = cf.get("NetworkSend", "UdpBroadcastIP")
	Cat001BroadcastPort = int(cf.get("NetworkSend","Cat001BroadcastPort"))
	Cat048BroadcastPort = int(cf.get("NetworkSend","Cat048BroadcastPort"))
	Cat062BroadcastPort = int(cf.get("NetworkSend","Cat062BroadcastPort"))
	Track9BroadcastPort = int(cf.get("NetworkSend","Track9BroadcastPort"))
	MeteoBroadcastPort = int(cf.get("NetworkSend","MeteoBroadcastPort"))
	IfplTomsBroadcastPort = int(cf.get("NetworkSend", "IfplTomsBroadcastPort"))
	TerminalAccIP = cf.get("NetworkSend","TerminalAccIP")
	TerminalAppIP = cf.get("NetworkSend", "TerminalAppIP")
	TerminalTwrIP = cf.get("NetworkSend", "TerminalTwrIP")
	CenterSaveIP = cf.get("NetworkSend", "CenterSaveIP")
	CenterSavePort048 = int(cf.get("NetworkSend", "CenterSavePort048"))
	CenterSavePort062 = int(cf.get("NetworkSend", "CenterSavePort062"))
	CenterSavePortOther = int(cf.get("NetworkSend", "CenterSavePortOther"))
	AGAMFPort = int(cf.get("NetworkSend", "AGAMFPort"))

	RecorderDataItemFrom = int(cf.get('RecorderDataItem','from'))
	RecorderDataItemTo = int(cf.get('RecorderDataItem', 'to'))

	ZipTime = cf.get("ScheduleTime","ZipTime")
	DelTime = cf.get("ScheduleTime", "DelTime")

	C062FilterSwitch = cf.get('C062Filter','Switch')
	#C062FilterContent = cf.get('C062Filter','Content')
	ContentSiftFromCat062 = cf.get('C062Filter','ContentSiftFromCat062')

	LogDisk = cf.get('Log','Disk')
	Radarpassageway = []
	passageway =cf.get('Radar','passageway').split(',')
	#ic(passageway,len(passageway))
	for i in passageway:
		if i:
			Radarpassageway.append(i)
	ThreadPoolMaxNum = int(cf.get('Thread','Max_num'))
	
	
def WriteToTXT(path,filename,data):
	try:
		with open(os.path.join(path,filename), 'a+') as f:
			f.write(data)
	except:
		os.makedirs(path)
		with open(os.path.join(path,filename), 'a+') as f:
			f.write(data)
	finally:
		pass

class RecvDataList:
	Flags = True
	IfplZDH = []
	FlightPlan = []
	Track9 =[]
	CAT062 = []
	OriginalRadar = []

class MHT40293():
	def __init__(self, data):
		self.data = data
	
	def IFPL(self):
		FPCTST, CALLSIGN, SECTOR, CFL, ADDR, ADEP, ADES, EOBD, EOBT, ADD, ATD, SSR, PSSR, ATA, ARCTYP, REG, TASK, IFPLID, TTLEET, ISCOUPLE = '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''
		ROUTE = []
		FlightPlan = {}
		
		for item in self.data:
			
			item_sub = item.strip().split(' ')
			
			if item_sub[0] == 'ARCID':
				if len(item_sub) == 2:
					CALLSIGN = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                  item_sub[1].strip())
			
			elif item_sub[0] == 'SECTOR':
				if len(item_sub) == 2:
					SECTOR = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                item_sub[1].strip())
			
			elif item_sub[0] == 'CFL':
				if len(item_sub) == 2:
					CFL = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
					if 'S' in CFL:
						CFL = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
						             item_sub[1].strip()).replace('S', '')
			
			elif item_sub[0] == 'ARCADDR':
				if len(item_sub) == 2:
					ADDR = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					              item_sub[1].strip())
			
			elif item_sub[0] == 'ADEP':
				if len(item_sub) == 2:
					ADEP = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ADES':
				if len(item_sub) == 2:
					ADES = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'EOBD':
				if len(item_sub) == 2:
					EOBD = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'EOBT':
				if len(item_sub) == 2:
					EOBT = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ADD':
				if len(item_sub) == 2:
					ADD = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ATD':
				if len(item_sub) == 2:
					ATD = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'SSRCODE':
				if len(item_sub) == 2:
					SSR = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'PSSRCODE':
				if len(item_sub) == 2:
					PSSR = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ATA':
				if len(item_sub) == 2:
					ATA = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ARCTYP':
				if len(item_sub) == 2:
					ARCTYP = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                item_sub[1].strip())
			
			# ROUTE的处理与其他不同
			elif item_sub[0] == 'ROUTE':
				
				if len(item_sub) >= 2:
					for i in range(len(item_sub)):
						if i >= 1 and item_sub[i]:
							Fix = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[i].strip())
							if '/' in Fix:
								Fix = Fix.split('/')[0]
							ROUTE.append(Fix)
			
			elif item_sub[0] == 'TASK':
				if len(item_sub) == 2:
					TASK = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'REG':
				if len(item_sub) == 2:
					REG = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'IFPLID':
				if len(item_sub) == 2:
					IFPLID = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ISCOUPLE':
				if len(item_sub) == 2:
					ISCOUPLE = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'FPCTST':
				if len(item_sub) == 2:
					FPCTST = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			
			elif item_sub[0] == 'TTLEET':
				if len(item_sub) == 2:
					TTLEET = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
					TTLEET = (int(TTLEET[0:2]) * 60) + int(TTLEET[2:4])
		
		FlightPlan.update({IFPLID: {'CALLSIGN': CALLSIGN, "SECTOR": SECTOR, "CFL": CFL, "ADDR": ADDR, "ADEP": ADEP,
		                            "ADES": ADES, "EOBD": EOBD, "EOBT": EOBT, "ADD": ADD, "ATD": ATD, "SSR": SSR,
		                            "PSSR": PSSR,
		                            "ATA": ATA, "ARCTYP": ARCTYP, "REG": REG, "TASK": TASK, "TTLEET": TTLEET,
		                            "ISCOUPLE": ISCOUPLE, "ROUTE": ROUTE, "FPCTST": FPCTST}})
		
		return (IFPLID, FlightPlan)
	
	def TOMS(self):
		# FPCTST, CALLSIGN, SECTOR, CFL, ADDR, ADEP, ADES, EOBD, EOBT, ADD, ATD, SSR, PSSR, ATA, ARCTYP, REG, TASK, IFPLID, TTLEET, ISCOUPLE = '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''
		IFPLID, CALLSIGN, ADEP, ADES, EOBD, \
		EOBT, SLT, CTOT, COBT, CTOD, \
		SURFACESTATUS, DRWY, PKC, SID = '', '', '', '', '', \
		                                '', '', '', '', '', \
		                                '', '', '', ''
		ROUTE = []
		FlightPlan = {}
		# ic(DataList)
		for item in self.data:
			
			item_sub = item.strip().split(' ')
			
			if item_sub[0] == 'IFPLID':
				if len(item_sub) == 2:
					IFPLID = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                item_sub[1].strip())
			
			
			elif item_sub[0] == 'ARCID':
				if len(item_sub) == 2:
					CALLSIGN = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                  item_sub[1].strip())
			
			elif item_sub[0] == 'ADEP':
				if len(item_sub) == 2:
					ADEP = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'ADES':
				if len(item_sub) == 2:
					ADES = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'EOBD':
				if len(item_sub) == 2:
					EOBD = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'EOBT':
				if len(item_sub) == 2:
					EOBT = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'SLT':
				if len(item_sub) == 2:
					SLT = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'COBT':
				if len(item_sub) == 2:
					COBT = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'CTOD':
				if len(item_sub) == 2:
					CTOD = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'CTOT':
				if len(item_sub) == 2:
					CTOT = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'PKC':
				if len(item_sub) == 2:
					PKC = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'SURFACESTATUS':
				if len(item_sub) == 2:
					SURFACESTATUS = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '',
					                       item_sub[1].strip())
			
			elif item_sub[0] == 'SID':
				if len(item_sub) == 2:
					SID = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
			
			elif item_sub[0] == 'DRWY':
				if len(item_sub) == 2:
					DRWY = re.sub(r'\\n|\\r\\n|\\r|\\n\\r|\n|\r|\r\n|\n\r| -|-| ', '', item_sub[1].strip())
		
		FlightPlan.update({IFPLID: {'CALLSIGN': CALLSIGN, "ADEP": ADEP, "ADES": ADES, "EOBD": EOBD, "EOBT": EOBT,
		                            "SLT": SLT, "COBT": COBT, "CTOD": CTOD, "CTOT": CTOT, "PKC": PKC, "SID": SID,
		                            "DRWY": DRWY, "SURFACESTATUS": SURFACESTATUS}})
		
		return (IFPLID, FlightPlan)

class ThreadPoolForHandle():
	def __init__(self):
		self.Pool = ThreadPoolExecutor(max_workers=CFG.ThreadPoolMaxNum)


class Track9UDPServer(socketserver.BaseRequestHandler):
	
	def handle(self):
		# bytes -->> str的解码放到线程中去处理，此处为确保尽快的从缓存中提取数据，直接存bytes
		# recvdata = bytes.decode(self.request[0], encoding='utf-8')
		RecvDataList.Track9.append(self.request[0])

class Track9TCPHandler(socketserver.BaseRequestHandler):
	"""
    The request handler class for our server.
    It is instantiated once per connection to the server, and must
    override the handle() method to implement communication to the
    client.
	"""

	def handle(self):
		try:
			pass
		except Exception as EPS:
			ic(EPS)


		finally:
			pass
	def ThreadLogging(self):
		pass

class IfplFromZDHUDPServer(socketserver.BaseRequestHandler):
	
	def handle(self):
		#bytes -->> str的解码放到线程中去处理，此处为确保尽快的从缓存中提取数据，直接存bytes
		#recvdata = bytes.decode(self.request[0], encoding='utf-8')
		RecvDataList.IfplZDH.append(self.request[0])

class Cat062UdpServer(socketserver.BaseRequestHandler):
	def handle(self):
		# bytes-->>hex-->>str的解码放到线程中去处理，此处为确保尽快的从缓存中提取数据，直接存bytes
		RecvDataList.CAT062.append(self.request[0])

class OriginalRadarUdpServer(socketserver.BaseRequestHandler):
	def handle(self):
		# bytes-->>hex-->>str的解码放到线程中去处理，此处为确保尽快的从缓存中提取数据，直接存bytes
		#recvdata = bytes.decode(self.request[0])
		RecvDataList.OriginalRadar.append(self.request[0])


class Connect:
	def __init__(self):
		
		if CFG.Switch_4029_3 == 'ON':
			threading.Thread(target=self.IfplZDHServer,daemon=True).start()
		if CFG.Switch_Cat048 == "ON":
			threading.Thread(target=self.OriginalRadarServer,daemon=True).start()
		if CFG.Switch_Cat062 == "ON":
			threading.Thread(target=self.Cat062Server,daemon=True).start()
		if CFG.Switch_FPFips == "ON":
			threading.Thread(target=self.FlightPlanFipsServer,daemon=True).start()
		if CFG.Swtich_Track9 == "ON":
			if CFG.PatternGetTrack9 =="UDP":
				threading.Thread(target=self.Track9UdpServer,daemon=True).start()
			elif CFG.PatternGetTrack9 == "TCP":
				threading.Thread(target=self.Track9TcpServer,daemon=True).start()
		if CFG.Swtich_MeteoMsg == "ON":
			threading.Thread(target=self.MeteoSerialServer,daemon=True).start()
		
	def IfplZDHServer(self,):
		HOST, PORT = "", CFG.IfplTomsUDPReceiverPort
		with socketserver.ThreadingUDPServer((HOST, PORT), IfplFromZDHUDPServer) as server:
			server.serve_forever()

	def OriginalRadarServer(self,):
		HOST, PORT = "", CFG.Cat048UDPReceiverPort
		with socketserver.ThreadingUDPServer((HOST, PORT), OriginalRadarUdpServer) as server:
			server.serve_forever()

	def Cat062Server(self,):
		HOST, PORT = "", CFG.Cat062UDPReceiverPort
		with socketserver.ThreadingUDPServer((HOST, PORT), Cat062UdpServer) as server:
			server.serve_forever()

	def FlightPlanFipsServer(self,):
		#http://195.168.1.203:5000/api/fips/gettoday
		while 1:
			try:
				response = urllib.request.urlopen(CFG.UrlGetFPFromFPFips)
				if response.status == 200:
					FP_FIPS = response.read().decode("utf-8").upper()
					#FP_FIPS_str = FP_FIPS.decode("utf-8").upper()
					RecvDataList.FlightPlan = eval(FP_FIPS, type('Dummy', (dict,), dict(__getitem__=lambda s, n: n))())
			
				else:
					pass
			except Exception as error:
				print('从%s获取FIPS航班计划出现错误:' % CFG.UrlGetFPFromFPFips, error)
			
				PresentTime = datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S.%f')
				PresentHour = datetime.datetime.now().strftime("%Y%m%d%H")
				PresentDay = datetime.datetime.now().strftime("%Y%m%d")
				path_error = os.path.join(CFG.LogDisk, 'AGAMF', 'Error', str(PresentDay), 'GetFPFromFIPS')
				LogFile_error = r'%s' % PresentHour + ".txt"
				DataToTXT_Error = "#error：%s#@ErrorTime:%s\n" % (error, PresentTime)
			
				#threading.Thread(target=WriteToTXT, args=(path_error, LogFile_error, DataToTXT_Error,),daemon=False).start()
				ThreadPoolForHandle().Pool.submit(WriteToTXT, path=path_error, filename=LogFile_error, data=DataToTXT_Error)
			finally:
				time.sleep(CFG.FipsGetInterval)
				
	def Track9UdpServer(self):
		HOST, PORT = "", CFG.Track9UdpPort
		with socketserver.ThreadingUDPServer((HOST, PORT), Track9UDPServer) as server:
			server.serve_forever()
	
	
	def Track9TcpServer(self):
		HOST , PORT = CFG.Track9TcpIP, CFG.Track9TcpPort
		with socketserver.ThreadingTCPServer((HOST, PORT),Track9TCPHandler) as server:
			server.serve_forever()
	
	
	def MeteoSerialServer(self):
		pass
	
	
			









def SendOrg048(data2send):
	CAT048Send_socket_O = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
	CAT048Send_socket_O.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
	#CAT048Send_socket_O.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
	CAT048send_bind_addr_O = ('192.168.1.159', 40048)

	CAT048Send_socket_O.sendto(data2send.encode('utf-8'), CAT048send_bind_addr_O)
	print('Cat048 Data Sened!')

def PostData2Center(TYP,Data,MSG):
	#Data 为dict,如空数据，则应改为：
	DataNeedPost = {"TYPE":TYP,"DATA":Data,"MSG":MSG}

	DataNeedPost_byte = json.dumps(DataNeedPost).encode('utf-8')

	#ic(type(DataNeedPost_byte))
	headers = {'Accept-Charset':'utf-8','Content-Type':'application/json'}
	t2 =time.clock()
	req =  urllib.request.Request(url='http://127.0.0.1:5000/api/ltfatdata/save-data', data=DataNeedPost_byte,headers=headers,method = 'POST')
	response = urllib.request.urlopen(req).read()
	print('post use times::',time.clock()-t2)
	print('the res:',response)
	print(DataNeedPost)

if __name__ =="__main__":

	#解决DOS命令行运行时，鼠标左键点击导致程序挂起的问题
	kernel32 = ctypes.windll.kernel32
	kernel32.SetConsoleMode(kernel32.GetStdHandle(-10), 128)
	######
	'''threading.Thread(target=Cat048SERVER,daemon=True).start()
	while 1:
		if RecvDataList.OriginalRadarRecv:
			data_str = RecvDataList.OriginalRadarRecv.pop(0).decode('UTF-8')
			#data_str = (binascii.hexlify(RecvDataList.OriginalRadarRecv.pop(0))).decode('utf-8')  # 方法一
			#print('data_str:',data_str,type(data_str))

			data_Decode = Asterix(data_str).Category_048()
			t1 = time.clock()
			threading.Thread(target=PostData2Center,args=('C048',data_Decode,'',),daemon=True).start()
			#PostData2Center(data_Decode)
			print('use time:',time.clock()-t1)

			#print('内存占用：',sys.getsizeof(RecvDataList.OriginalRadarRecv))
			#threading.Thread(target=SendOrg048,args=(json.dumps(data_Decode,ensure_ascii=False),),daemon=True).start()
		else:
			time.sleep(0.0001)'''
	#ic(config().MeteoAirport)
	#TN:1545
	#PostData2Center('ERROR',{},'This is a test data')
	'''a = "ZCZC\n-TITLE ICNL\n-SOURCE NUMEN3000@LES.ZGHA\n-FILTIM 190057\n-IFPLID 1635619965\n-ARCID CES9881\n-ADEP ZGKL\n-ADES ZSYT\n-ARCTYP A320\n-CEQPT \n-SEQPT \n-CFL \n-ISCOUPLE N\n-EOBD 20211103\n-EOBT 0100\n-SECTOR \n-SECDEST \n-ETA 20211103074359\n-FPCTST FIN\n-NBARC 1\n-SID \n-STAR \n-PSSRCODE \n-SSRCODE \n-TXT \n-WKTRC M\n-TTLEET 0245\n-FLTRUL \n-FLTTYP \n-ALTRNT1 \n-ALTRNT2 \n-DRWY \n-ARWY \n-ROUTE ONEMI P471 P246 NUPTI VIGIS PUKAD LLC BEMTA P169 GOSMA LKO UBTAG HOK UBGIV P538 ENLAB TULRA IGMIG VINIG P129 OBMEP OKTOX P98 DOSKU VADMO WXI EKORO GULEK MUMUN P229 YQG OKALI WFG P584 GUTVO P585 P586 HCH\n-RFL S1550\n-SPEED N0410\n-XFL \n-PKC \n-TASK W/Z\n-UACID \n\n-TEAM18 \n-COM \n-DAT \n-NAV \n-OPR \n-PER \n-RALT \n-ARCADDR \n-REG \n-SEL \n-RMK \n-STS \n-PBN \nNNNN"
	
	b = MHT40293(a.split('-')).IFPL()
	ic(b)'''
	Connect()
	
