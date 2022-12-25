# -*-coding:UTF-8 -*-
#########################################################################
#  Copyright: ATMB-HN. All rights reserved.
#  Created By :Anle Pi@ 2020.08
#
#########################################################################

from textwrap import wrap
from math import atan2, degrees
from decimal import Decimal, ROUND_HALF_UP



def int2hex(n):
    """Convert a integer to hexadecimal string."""
    # strip 'L' for python 2
    return hex(n)[2:].rjust(6, "0").upper().rstrip("L")

def hex2bin(hexstr):
    """Convert a hexdecimal string to binary string, with zero fillings."""
    num_of_bits = len(hexstr) * 4
    binstr = bin(int(hexstr, 16))[2:].zfill(int(num_of_bits))
    return binstr

def hex2int(hexstr):
    """Convert a hexdecimal string to integer."""
    return int(hexstr, 16)

def bin2int(binstr):
    """Convert a binary string to integer."""
    return int(binstr, 2)

def bin2hex(hexstr):
    """Convert a hexdecimal string to integer."""
    return int2hex(bin2int(hexstr))

def bin2oct(binstr):
    return oct(int(binstr, 2))[2:]

def callsign(x):
    chars = "#ABCDEFGHIJKLMNOPQRSTUVWXYZ#####################0123456789######"
    cs_bin = hex2bin(x)
    # print(cs_bin)
    cs = ""
    cs += chars[bin2int(cs_bin[0:6])]
    cs += chars[bin2int(cs_bin[6:12])]
    cs += chars[bin2int(cs_bin[12:18])]
    cs += chars[bin2int(cs_bin[18:24])]
    cs += chars[bin2int(cs_bin[24:30])]
    cs += chars[bin2int(cs_bin[30:36])]
    cs += chars[bin2int(cs_bin[36:42])]
    cs += chars[bin2int(cs_bin[42:48])]
    cs = cs.replace("#", "")
    # print(cs)
    return cs

def JudgeFSPEC(Str_hex, x, y):
    fspec_tem = Str_hex[x:y]
    fspec_tem_bin = str(hex2bin(fspec_tem))
    len_tem = len(fspec_tem)
    while fspec_tem_bin[-1] == '1':
        fspec_tem += Str_hex[x + len_tem:y + len_tem]
        fspec_tem_bin = str(hex2bin(fspec_tem))
        len_tem = len(fspec_tem)
        if fspec_tem_bin[-1] == '0':
            break
    return (fspec_tem, fspec_tem_bin)


##################################################################
# FOR BDS
# BDS 2,0 Aircraft identification
# BDS 2,1 Aircraft and airline registration markings
# BDS 4,0 Selected vertical intention
# BDS 4,4 Meteorological routine air report
# BDS 5,0 Track and turn report
# BDS 6,0 Heading and speed report
##################################################################

def data(msg):
    return msg[8:-6]

def allzeros(msg):
    d = hex2bin(data(msg))
    if bin2int(d) > 0:
        return False
    else:
        return True

def is30(msg):
    if allzeros(msg):
        return False

    d = hex2bin(data(msg))

    if d[0:8] != "00110000":
        return False

    # threat type 3 not assigned
    if d[28:30] == "11":
        return False

    # reserved for ACAS III, in far future
    if bin2int(d[15:22]) >= 48:
        return False

    return True

def BDS30(x):
    if is30(x) == True:
        x_wrap = wrap(x, 2)
        Hex2Ascii = ''
        for x_one in x_wrap:
            x_one_chr = chr(hex2int(x_one))
            Hex2Ascii += x_one_chr
        return ({'BDS30': Hex2Ascii})

def BDS40(x):
    bds_40 = {}
    ModeS_bin = str(hex2bin(x))
    if ModeS_bin[0] == "1":
        MCPorFCUSelectAltitude_m = int(bin2int(ModeS_bin[1:13]) * 16 * 0.3048)
        bds_40.update({'MCPorFCUSelectAltitude_m': MCPorFCUSelectAltitude_m})

    if ModeS_bin[13] == "1":
        FMSSelectAltitude_m = int(bin2int(ModeS_bin[14:26]) * 16 * 0.3048)
        bds_40.update({'FMSSelectAltitude_m': FMSSelectAltitude_m})

    if ModeS_bin[26] == "1":
        BarometricPressureSet_mb = bin2int(ModeS_bin[27:39]) * 0.1 + 800
        bds_40.update({'BarometricPressureSet_mb': BarometricPressureSet_mb})

    # bds_40.update({'BDS40':{'MCPorFCUSelectAltitude_m':MCPorFCUSelectAltitude_m,'FMSSelectAltitude_m':FMSSelectAltitude_m,'BarometricPressureSet_mb':BarometricPressureSet_mb}})
    return ({'BDS40': bds_40})

def BDS50(data):
    bds_50 = {}
    data_bin = str(hex2bin(data))
    if data_bin[0] == "1":
        if data_bin[1] == "1":
            RollAngle = (bin2int(data_bin[2:11]) - 512) * 45 / 256
        else:
            RollAngle = bin2int(data_bin[2:11]) * 45 / 256
        bds_50.update({'RollAngle': RollAngle})

    if data_bin[11] == "1":
        if data_bin[12] == "1":
            TrueTrackAngle = (bin2int(data_bin[13:23]) - 1024) * 90 / 512
        else:
            TrueTrackAngle = bin2int(data_bin[13:23]) * 90 / 512
        bds_50.update({'TrueTrackAngle': TrueTrackAngle})

    if data_bin[23] == '1':
        GroundSpeed = bin2int(data_bin[24:34]) * 2
        bds_50.update({'GroundSpeed': GroundSpeed})

    if data_bin[34] == '1':
        if data_bin[35] == '1':
            TrackAngleRate = (bin2int(data_bin[36:45]) - 512) * 8 / 256
        else:
            TrackAngleRate = bin2int(data_bin[36:45]) * 8 / 256
        bds_50.update({'TrackAngleRate': TrackAngleRate})

    if data_bin[45] == "1":
        TrueAirspeed = bin2int(data_bin[46:]) * 2
        bds_50.update({'TrackAngleRate': TrueAirspeed})

    # bds_50.update({'BDS50':{'RollAngle': RollAngle, 'TrueTrackAngle': TrueTrackAngle, "GroundSpeed": GroundSpeed,'TrackAngleRate': TrackAngleRate, "TrueAirspeed": TrueAirspeed}})
    return ({'BDS50': bds_50})

def BDS60(data):
    bds_60 = {}
    data_bin = str(hex2bin(data))
    if data_bin[0] == "1":
        if data_bin[1] == "1":
            MagneticHeading = (bin2int(data_bin[2:12]) - 1024) * 90 / 512
        else:
            MagneticHeading = bin2int(data_bin[2:12]) * 90 / 512
        bds_60.update({'MagneticHeading': MagneticHeading})

    if data_bin[12] == "1":
        IndicatedAirspeed = bin2int(data_bin[13:23])
        bds_60.update({'IndicatedAirspeed': IndicatedAirspeed})

    if data_bin[23] == "1":
        MachNumber = bin2int(data_bin[24:34]) * 2.048 / 512
        bds_60.update({'MachNumber': MachNumber})

    if data_bin[34] == "1":
        if data_bin[35] == "1":
            BarometricAltitudeRate = (bin2int(data_bin[36:45]) - 512) * 32
        else:
            BarometricAltitudeRate = bin2int(data_bin[36:45]) * 32
        bds_60.update({'BarometricAltitudeRate': BarometricAltitudeRate})

    if data_bin[45] == "1":
        if data_bin[46] == "1":
            InertialAltitudeRate = (bin2int(data_bin[47:]) - 512) * 32
        else:
            InertialAltitudeRate = bin2int(data_bin[47:]) * 32
        bds_60.update({'InertialAltitudeRate': InertialAltitudeRate})

    # bds_60.update({'BDS60':{'MagneticHeading': MagneticHeading, 'IndicatedAirspeed': IndicatedAirspeed, "MachNumber": MachNumber,'BarometricAltitudeRate': BarometricAltitudeRate, "InertialAltitudeRate": InertialAltitudeRate}})
    return ({'BDS60': bds_60})

def Category001_track(str_data, fspec_hex_001, fspec_bin_001, LenOf_010, LenOf_020, x):
    # print(str,fspec_hex,fspec_bin,Len_010,Len_020)
    DataCategory001 = str_data
    FSPEC_Hex = fspec_hex_001
    FSPEC_Bin = fspec_bin_001
    LEN010 = LenOf_010
    LEN020 = LenOf_020
    LEN_FSPEChex = len(FSPEC_Hex)
    InfoCategory001Track = {}
    # print('Category001_track中收到的数据：',str_data, fspec_hex_001, fspec_bin_001, LenOf_010, LenOf_020, x)

    # global LEN161,LEN040,LEN042,LEN200,LEN070,LEN090,LEN141,LEN130,LEN131,LEN120,LEN170
    LEN161, LEN040, LEN042, LEN200, LEN070, LEN090, LEN141, LEN130, LEN131, LEN120, LEN170 = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    LEN210, LEN050, LEN080, LEN100, LEN060, LEN030, Len001_SectionOne = 0, 0, 0, 0, 0, 0, 0
    for bit_number in range(len(FSPEC_Bin)):
        if bit_number == 2:  # /161 Track/Plot Number
            if FSPEC_Bin[bit_number] == "1":
                # I001_161 = hex2int(DataCategory001[x+LEN_FSPEChex+LEN010+LEN020:x+4+LEN_FSPEChex+LEN010+LEN020])
                # print('/161：',int2hex(I001_161))
                LEN161 = 4

        elif bit_number == 3:  # /040 Measured Position in Polar Coordinates
            if FSPEC_Bin[bit_number] == "1":
                LEN040 = 8
                # RHO_bin = hex2bin(DataCategory001[x+LEN_FSPEChex+LEN010+LEN020+LEN161:x+4+LEN_FSPEChex+LEN010+LEN020+LEN161])
                # THETA_bin = hex2bin(DataCategory001[x+4+LEN_FSPEChex+LEN010+LEN020+LEN161:x+8+LEN_FSPEChex+LEN010+LEN020+LEN161])
                # if RHO_bin[0] == "0":
                #	RHO = bin2int(RHO_bin)/128
                # else:
                #	RHO =(bin2int(RHO_bin[1:])-32768)/128

                # if THETA_bin[0] == "0":
                #	THETA = bin2int(THETA_bin)* 360 / 65536
                # else:
                #	THETA =(bin2int(THETA_bin[1:])-32768)* 360 / 65536

                RHO = hex2int(DataCategory001[
                              x + LEN_FSPEChex + LEN010 + LEN020 + LEN161:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161]) / 128
                THETA = hex2int(DataCategory001[
                                x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161:x + 8 + LEN_FSPEChex + LEN010 + LEN020 + LEN161]) * 360 / 65536

                InfoCategory001Track.update({'PolarCoordinates': {"RHO_NM": RHO, "THETA_Degree": THETA}})

        elif bit_number == 4:  # /042 Calculated Position in Cartesian Coordinates
            if FSPEC_Bin[bit_number] == "1":
                LEN042 = 8
                # COX_NM_bin = hex2bin(DataCategory001[x+LEN_FSPEChex+LEN010+LEN020+LEN161+LEN040:x+4+LEN_FSPEChex+LEN010+LEN020+LEN161+LEN040])
                # COY_NM_bin = hex2bin(DataCategory001[x+4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040:x+8+ LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040])
                # if COX_NM_bin[0] == "0":
                #	CartesianCoOrdinatesX_NM = bin2int(COX_NM_bin)/256
                # else:
                #	CartesianCoOrdinatesX_NM = (bin2int(COX_NM_bin[1:])-32768)/256

                # if COY_NM_bin[0] == "0":
                #	CartesianCoOrdinatesY_NM = bin2int(COY_NM_bin)/256
                # else:
                #	CartesianCoOrdinatesY_NM = (bin2int(COY_NM_bin[1:]) - 32768) / 256

                CartesianCoOrdinatesX_NM = DataCategory001[
                                           x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040]
                CartesianCoOrdinatesY_NM = DataCategory001[
                                           x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040:x + 8 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040]
                InfoCategory001Track.update({'CartesianCoOrdinates_hex': {'X_nm_hex': CartesianCoOrdinatesX_NM,
                                                                          'Y_nm_hex': CartesianCoOrdinatesY_NM}})

        elif bit_number == 5:  # /200 Calculated Position in Cartesian Coordinates
            if FSPEC_Bin[bit_number] == "1":
                LEN200 = 8
                GroundSpeed_kt = hex2int(DataCategory001[
                                         x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042]) * 0.2197265625
                Heading_degree = hex2int(DataCategory001[
                                         x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042:x + 8 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042]) * 360 / 65536
                # a = DataCategory001[6+LEN_FSPEChex+LEN010+LEN020+LEN161+LEN040+LEN042:10+LEN_FSPEChex+LEN010+LEN020+LEN161+LEN040+LEN042]
                # a_bin = hex2bin(a)
                # a_bin_reversted = a_bin[::-1]
                # GroundSpeed_Reversted_kt = bin2int(a_bin_reversted)* 0.2197265625
                # print('GroundSpeed_Bin:  %s  , | ,GroundSpeed_kt:  %s'%(a_bin,GroundSpeed_kt))
                # print('GroundSpeed_Bin_Reversted: %s , | , GroundSpeed_Reversted_kt :  %s'%(a_bin_reversted,GroundSpeed_Reversted_kt))
                InfoCategory001Track.update(
                    {'VelocityInPolar': {'GroundSpeed_kt': GroundSpeed_kt, 'Heading_degree': Heading_degree}})

        elif bit_number == 6:  # /070 Mode-3/A Code in Octal Representation
            if FSPEC_Bin[bit_number] == "1":
                LEN070 = 4
                I001_070_hex = DataCategory001[
                               x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200]
                # print ('I001/070:',I001_070_hex)
                I001_070_bin = hex2bin(I001_070_hex)
                I001_070_Mode3ACode = ''
                if I001_070_bin[0] == "0" and I001_070_bin[1] == '0':
                    I001_070_Mode3ACode_bin = wrap(I001_070_bin[4:], 3)
                    for ii in I001_070_Mode3ACode_bin:
                        i_oct = bin2oct(ii)
                        # print('i_oct',i_oct)
                        I001_070_Mode3ACode += i_oct
                    InfoCategory001Track.update({'Mode3ACode': I001_070_Mode3ACode})
                else:
                    InfoCategory001Track.update({'Mode3ACode': 'NotUseable'})

        elif bit_number == 8:  # /090 Mode-C Code in Binary Representation
            if FSPEC_Bin[bit_number] == "1":
                LEN090 = 4
                I001_090_hex = DataCategory001[
                               x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070]
                I001_090_bin = hex2bin(I001_090_hex)
                if I001_090_bin[0] == "0" and I001_090_bin[1] == '0':
                    if I001_090_bin[2] == "0":
                        FlightLevel_m = int((bin2int(I001_090_bin[2:]) * 25 * 0.3048))
                    else:
                        FlightLevel_m = int((bin2int(I001_090_bin[3:]) - 8192) * 25 * 0.3048)
                # Flight_reversed = int((bin2int(I001_090_bin[2:][::-1]) * 25 * 0.3048))
                # print('Flight_m: %s ,|| , Flight-Reversted_m: %s'%(FlightLevel_m,Flight_reversed))
                else:
                    # FlightLevel_ft = "NULL"
                    FlightLevel_m = "NULL"
                InfoCategory001Track.update({'FlightLevel_m': FlightLevel_m})

        elif bit_number == 9:  # /141 Truncated Time of Day
            if FSPEC_Bin[bit_number] == "1":
                LEN141 = 4
                I001_141_hex = DataCategory001[
                               x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090:x + 4 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090]
                # print(I001_141_hex)
                I001_141_hour = int(hex2int(I001_141_hex) / 128 / 3600)
                I001_141_min = int((hex2int(I001_141_hex) / 128 / 3600 - I001_141_hour) * 60)
                I001_141_second = ((hex2int(I001_141_hex) / 128 / 3600 - I001_141_hour) * 60 - I001_141_min) * 60
                I001_141_TimeOfDay = '%s:%s:%s' % (I001_141_hour, I001_141_min, I001_141_second)
                InfoCategory001Track.update({'TimeOfDay': I001_141_TimeOfDay})



        elif bit_number == 10:  # /130 Radar Plot Characteristics
            if FSPEC_Bin[bit_number] == "1":
                I001_130 = JudgeFSPEC(DataCategory001,
                                      x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141,
                                      x + 2 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141)

                # print('I_001_130:',I001_130)
                LEN130 = len(I001_130[0])

        elif bit_number == 11:  # /131 Received Power
            if FSPEC_Bin[bit_number] == "1":
                LEN131 = 2

        elif bit_number == 12:  # /120  Measured Radial Doppler Speed
            if FSPEC_Bin[bit_number] == "1":
                LEN120 = 2

        elif bit_number == 13:  # /170  Track Status
            if FSPEC_Bin[bit_number] == "1":
                I001_170 = JudgeFSPEC(DataCategory001,
                                      x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120,
                                      x + 2 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120)
                LEN170 = len(I001_170[0])
                I001_170_bin_list = wrap(I001_170[1], 8)
                for Octet_no in range(len(I001_170_bin_list)):
                    if Octet_no == 0:
                        if I001_170_bin_list[Octet_no][0] == "0":
                            CON = "Confirmed"
                        else:
                            CON = "Initial"
                        if I001_170_bin_list[Octet_no][1] == "0":
                            RAD = "PSR"
                        else:
                            RAD = "SSR"
                        InfoCategory001Track.update({'CON': CON, 'RAD': RAD})

        elif bit_number == 14:  # /210  Track Quality
            if FSPEC_Bin[bit_number] == "1":
                I001_210 = JudgeFSPEC(DataCategory001,
                                      x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120 + LEN170,
                                      x + 2 + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120 + LEN170)
                LEN210 = len(I001_210[0])

        elif bit_number == 16:  # FRN 15/050  Mode-2 Code in Octal Representation --2 octets
            if FSPEC_Bin[bit_number] == "1":
                LEN050 = 4

        elif bit_number == 17:  # FRN 16/080  Mode-3/A Code Confidence Indicator --2 octets
            if FSPEC_Bin[bit_number] == "1":
                LEN080 = 4

        elif bit_number == 18:  # FRN 17/100  Mode-C Code and Code Confidence Indicator --4 octets
            if FSPEC_Bin[bit_number] == "1":
                LEN100 = 8

        elif bit_number == 19:  # FRN 18/060  Mode-2 Code Confidence Indicator --2 octets
            if FSPEC_Bin[bit_number] == "1":
                LEN060 = 4

        elif bit_number == 20:  # FRN 19/030  Warning/Error Conditions --1+ octets
            if FSPEC_Bin[bit_number] == "1":
                begin001_030 = x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120 + LEN170 + LEN210 + LEN050 + LEN080 + LEN100 + LEN060
                I001_030 = JudgeFSPEC(DataCategory001, begin001_030, begin001_030 + 2)
                LEN030 = len(I001_030[0])
        Len001_SectionOne = x + LEN_FSPEChex + LEN010 + LEN020 + LEN161 + LEN040 + LEN042 + LEN200 + LEN070 + LEN090 + LEN141 + LEN130 + LEN131 + LEN120 + LEN170 + LEN210 + LEN050 + LEN080 + LEN100 + LEN060 + LEN030
    return (InfoCategory001Track, Len001_SectionOne)

def Category062_track(data, x):
    data_str = data
    # InfoDecodeInCat062 = {}
    InfoDecodeInCat062_one = {}
    # global Len062_010,Len062_015, Len062_070, Len062_105, Len062_100, Len062_185, Len062_210, Len062_060, Len062_245, Len062_380_SubADR, Len062_380_SubID, Len062_380_SubMHG, Len062_380_SubIAS, Len062_380_SubTAS, Len062_380_SubSAL, Len062_380_SubFSS, Len062_380_SubTIS, Len062_380_SubTID, Len062_380_SubCOM, Len062_380_SubSAB, Len062_380_SubACS, Len062_380_SubBVR, Len062_380_SubGVR, Len062_380_SubRAN, Len062_380_SubTAR, Len062_380_SubTAN, Len062_380_SubGSP, Len062_380_SubVUN, Len062_380_SubMET, Len062_380_SubEMC, Len062_380_SubPOS, Len062_380_SubGAL, Len062_380_SubPUN, Len062_380_SubMB, Len062_380_SubIAR, Len062_380_SubMAC, Len062_380_fspec, Len062_380_SubBPS,Len062_380, Len062_040, Len062_080, Len062_290, Len062_200, Len062_295, Len062_136, Len062_130, Len062_220, Len062_135, Len062_390_SubTAG, Len062_390_SubCSN, Len062_390_SubIFI, Len062_390_SubFCT, Len062_390_SubTAC, Len062_390_SubWTC, Len062_390_SubDEP, Len062_390_SubDST, Len062_390_SubRDS, Len062_390_SubCFL, Len062_390_SubCTL, Len062_390_SubTOD, Len062_390_SubAST, Len062_390_SubSTS, Len062_390_SubSTD, Len062_390_SubSTA, Len062_390_fspec, Len062_390_SubPEM, Len062_390_SubPEC,Len062_390,Len062_270, Len062_300, Len062_110_Sub_SUM, Len062_110_Sub_PMN, Len062_110_Sub_POS, Len062_110_Sub_GA, Len062_110_Sub_EM1, Len062_110_Sub_TOS, Len062_110_Sub_XP, Len062_110, Len062_120, Len062_510, Len_I062_500_Sub, Len062_500
    Len062_010, Len062_015, Len062_070, Len062_105, Len062_100, Len062_185, Len062_210, Len062_060, Len062_245 = 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_380_SubADR, Len062_380_SubID, Len062_380_SubMHG, Len062_380_SubIAS, Len062_380_SubTAS, Len062_380_SubSAL, Len062_380_SubFSS, Len062_380_SubTIS, Len062_380_SubTID, Len062_380_SubCOM = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_380_SubSAB, Len062_380_SubACS, Len062_380_SubBVR, Len062_380_SubGVR, Len062_380_SubRAN, Len062_380_SubTAR, Len062_380_SubTAN, Len062_380_SubGSP, Len062_380_SubVUN, Len062_380_SubMET = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_380_SubEMC, Len062_380_SubPOS, Len062_380_SubGAL, Len062_380_SubPUN, Len062_380_SubMB, Len062_380_SubIAR, Len062_380_SubMAC, Len062_380_fspec, Len062_380_SubBPS = 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_380, Len062_040, Len062_080, Len062_290, Len062_200, Len062_295, Len062_136, Len062_130, Len062_220, Len062_135 = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_390_SubTAG, Len062_390_SubCSN, Len062_390_SubIFI, Len062_390_SubFCT, Len062_390_SubTAC, Len062_390_SubWTC, Len062_390_SubDEP, Len062_390_SubDST, Len062_390_SubRDS, Len062_390_SubCFL = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_390_SubCTL, Len062_390_SubTOD, Len062_390_SubAST, Len062_390_SubSTS, Len062_390_SubSTD, Len062_390_SubSTA, Len062_390_fspec, Len062_390_SubPEM, Len062_390_SubPEC, Len062_390 = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len062_270, Len062_300, Len062_110, Len062_120, Len062_510, Len_I062_500_Sub, Len062_500, Len062_340 = 0, 0, 0, 0, 0, 0, 0, 0
    Len062_110_Sub_SUM, Len062_110_Sub_PMN, Len062_110_Sub_POS, Len062_110_Sub_GA, Len062_110_Sub_EM1, Len062_110_Sub_TOS, Len062_110_Sub_XP = 0, 0, 0, 0, 0, 0, 0
    # if self.JudgeDataLength() == True:
    fspec_062 = JudgeFSPEC(data_str, x, x + 2)
    # print ('FSPEC:',fspec_062)
    fspec_062_hex = fspec_062[0]
    fspec_062_bin = fspec_062[1]
    LenFspecHex_062 = len(fspec_062_hex)
    for bit_i in range(len(fspec_062_bin)):
        if bit_i == 0:  # /010 Data Source Identifier
            if fspec_062_bin[bit_i] == "1":
                I062_010_DataSourceIdentifier = data_str[x + LenFspecHex_062:x + 4 + LenFspecHex_062]
                Len062_010 = 4
                '''if 'SAC_SIC' in Received.ContentSiftFromCat062:
                    InfoDecodeInCat062_one.update({'SAC_SIC': I062_010_DataSourceIdentifier})'''

        elif bit_i == 2:  # /015 Service Identification
            if fspec_062_bin[bit_i] == "1":
                Len062_015 = 2

        elif bit_i == 3:  # /070 Time Of Track Information
            if fspec_062_bin[bit_i] == "1":
                Len062_070 = 6
                '''if 'TimeOfDay' in Received.ContentSiftFromCat062:
                    I062_070_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015:x + 6 + LenFspecHex_062 + Len062_010 + Len062_015]
                    # print ('FRN2:',I048_140_hex)

                    I062_070_hour = int(hex2int(I062_070_hex) / 128 / 3600)
                    I062_070_min = int((hex2int(I062_070_hex) / 128 / 3600 - I062_070_hour) * 60)
                    I062_070_second = ((hex2int(I062_070_hex) / 128 / 3600 - I062_070_hour) * 60 - I062_070_min) * 60
                    I062_070_TimeOfDay = '%s:%s:%s' % (I062_070_hour, I062_070_min, I062_070_second)

                    InfoDecodeInCat062_one.update({'TimeOfDay': I062_070_TimeOfDay})'''

        elif bit_i == 4:  # /105 Calculated Track Position (WGS84)
            if fspec_062_bin[bit_i] == "1":
                Len062_105 = 16
                #if 'WGS-84' in Received.ContentSiftFromCat062:
                Latitude_wgs84_bin = hex2bin(data_str[
                                                 x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070])
                Longitude_wgs84_bin = hex2bin(data_str[
                                                  x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070:x + 16 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070])
                if Latitude_wgs84_bin[0] == "0":
                    Latitude_wgs84 = bin2int(Latitude_wgs84_bin) * 180 / 33554432
                else:
                    Latitude_wgs84 = (bin2int(Latitude_wgs84_bin[1:]) - 2147483648) * 180 / 33554432

                if Longitude_wgs84_bin[0] == "0":
                    Longitude_wgs84 = bin2int(Longitude_wgs84_bin) * 180 / 33554432
                else:
                    Longitude_wgs84 = (bin2int(Longitude_wgs84_bin[1:]) - 2147483648) * 180 / 33554432
                    # Latitude_wgs84 = hex2int(data_str[x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070]) * 180 / 33554432
                    # Longitude_wgs84 = hex2int(data_str[x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070:x + 16 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070]) * 180 / 33554432
                    # print('LAT-84: %s , LOG-84 : %s'%(Latitude_wgs84,Longitude_wgs84))
                InfoDecodeInCat062_one.update({'WGS84': {'LAT': Latitude_wgs84, 'LON': Longitude_wgs84}})

        elif bit_i == 5:  # /100 Calculated Track Position (Cartesian)
            if fspec_062_bin[bit_i] == "1":
                Len062_100 = 12
                '''if 'Cartesian' in Received.ContentSiftFromCat062:
                    X_m_Cartesian_bin = hex2bin(data_str[
                                                x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105:x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105])
                    Y_m_Cartesian_bin = hex2bin(data_str[
                                                x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105:x + 12 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105])

                    if X_m_Cartesian_bin[0] == "0":
                        X_m_Cartesian = bin2int(X_m_Cartesian_bin) * 0.5
                    else:
                        X_m_Cartesian = (bin2int(X_m_Cartesian_bin[1:]) - 8388608) * 0.5

                    if Y_m_Cartesian_bin[0] == "0":
                        Y_m_Cartesian = bin2int(Y_m_Cartesian_bin) * 0.5
                    else:
                        Y_m_Cartesian = (bin2int(Y_m_Cartesian_bin[1:]) - 8388608) * 0.5

                    # X_m_Cartesian = hex2int(data_str[x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105:x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105]) * 0.5
                    # Y_m_Cartesian = hex2int(data_str[x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105:x + 12 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105]) * 0.5
                    # print('X: %s , Y: %s'%(X_m_Cartesian,Y_m_Cartesian))

                    InfoDecodeInCat062_one.update({'Cartesian': {'X_m': X_m_Cartesian, 'Y_m': Y_m_Cartesian}})'''

        elif bit_i == 6:  # /185 Calculated Track Velocity (Cartesian)
            if fspec_062_bin[bit_i] == "1":
                Len062_185 = 8
                #if 'Vcartesian' in Received.ContentSiftFromCat062:
                Vx_Cartesian_bin = hex2bin(data_str[
                                               x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100])
                Vy_Cartesian_bin = hex2bin(data_str[
                                               x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100])

                if Vx_Cartesian_bin[0] == "0":
                    Vx_Cartesian_MeterSecond = bin2int(Vx_Cartesian_bin) * 0.25
                else:
                    Vx_Cartesian_MeterSecond = (bin2int(Vx_Cartesian_bin[1:]) - 32768) * 0.25

                if Vy_Cartesian_bin[0] == "0":
                    Vy_Cartesian_MeterSecond = bin2int(Vy_Cartesian_bin) * 0.25
                else:
                    Vy_Cartesian_MeterSecond = (bin2int(Vy_Cartesian_bin[1:]) - 32768) * 0.25

                Speed = (((Vx_Cartesian_MeterSecond ** 2) + (Vy_Cartesian_MeterSecond ** 2)) ** 0.5) * 3.6
                heading = degrees(atan2(Vx_Cartesian_MeterSecond, Vy_Cartesian_MeterSecond))
                if heading <= 0:
                    heading += 360
                heading = str(heading).zfill(3)
                # print('Speed',Speed)

                Speed = int(Decimal(str(Speed)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))
                heading = int(Decimal(str(heading)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))

                InfoDecodeInCat062_one.update({'Speed': Speed, 'Heading': heading})

        elif bit_i == 8:  # /210 Calculated Acceleration (Cartesian)
            if fspec_062_bin[bit_i] == "1":
                Len062_210 = 4
                #if 'Acartesian' in Received.ContentSiftFromCat062:
                Ax_Cartesian_MeterSecondSquare = hex2int(data_str[
                                                             x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185]) * 0.25
                Ay_Cartesian_MeterSecondSquare = hex2int(data_str[
                                                             x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185]) * 0.25
                InfoDecodeInCat062_one.update({'Acartesian': {'X_m/s2': Ax_Cartesian_MeterSecondSquare,
                                                                  'Y_m/s2': Ay_Cartesian_MeterSecondSquare}})
            # print (InfoDecodeInCat062_one)

        elif bit_i == 9:  # /060 Track Mode 3/A Code
            if fspec_062_bin[bit_i] == "1":
                Len062_060 = 4
                #if 'Mode_3A' in Received.ContentSiftFromCat062:
                I062_060_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210]
                Mode_3A_062_bin = hex2bin(I062_060_hex)[4:]
                Mode_3A_062 = ""
                Mode_3A_062_bin_list = wrap(Mode_3A_062_bin, 3)
                for section_oct in Mode_3A_062_bin_list:
                    Mode_3A_062 += str(bin2oct(section_oct))
                InfoDecodeInCat062_one.update({'Mode_3A': Mode_3A_062})

        elif bit_i == 10:  # /245 Target Identification
            if fspec_062_bin[bit_i] == "1":
                Len062_245 = 14
                '''if 'TargetIdentifition' in Received.ContentSiftFromCat062:
                    I062_245_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060:x + 14 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060]
                    I062_245_bin = hex2bin(I062_245_hex)
                    if I062_245_bin[0:2] != '11':
                        TargetIdentifition_062 = callsign(I062_245_hex[2:])
                        # print(TargetIdentifition_062)
                        InfoDecodeInCat062_one.update({'TargetIdentifition': TargetIdentifition_062})
                    else:
                        InfoDecodeInCat062_one.update({'TargetIdentifition': 'Invaild'})'''

        elif bit_i == 11:  # /380 Aircraft Derived Data
            if fspec_062_bin[bit_i] == "1":
                AircraftDerivedData = {}
                Item380Fspec = JudgeFSPEC(data_str,
                                          x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245,
                                          x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245)
                Item380Fspec_hex = Item380Fspec[0]
                Len062_380_fspec = len(Item380Fspec_hex)
                Item380Fspec_bin = Item380Fspec[1]

                for bit_num in range(len(Item380Fspec_bin)):
                    # Compound Data Item, comprising a primary subfield of up to four octets,followed by the indicated subfields.
                    # the bit-num is:
                    # ADR ID MHG IAS TAS SAL FSS FX TIS TID COM SAB ACS BVR GVR FX RAN TAR TAN GSP VUN MET EMC FX POS GAL PUN MB IAR MAC BPS FX
                    if bit_num == 0:  # Subfield # 1: ADR Target Address 3 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubADR = 6
                            #if 'Address' in Received.ContentSiftFromCat062:
                            Subfield_ADR_hex = data_str[
                                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec:x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec]
                            AircraftDerivedData.update({'Address': Subfield_ADR_hex})


                    elif bit_num == 1:  # Subfield # 2: ID Target Identification 6 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubID = 12
                            #if 'ID' in Received.ContentSiftFromCat062:
                            Subfield_ID_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR:x + 12 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR]
                            Subfield_ID_Ascii = callsign(Subfield_ID_hex)
                            AircraftDerivedData.update({'ID': Subfield_ID_Ascii})

                    elif bit_num == 2:  # Subfield # 3: MHG Magnetic Heading 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubMHG = 4
                            #if 'MHG' in Received.ContentSiftFromCat062:
                            Subfield_MHG = hex2int(data_str[
                                                       x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID]) * 360 / 65536
                            AircraftDerivedData.update({'MHG': Subfield_MHG})

                    elif bit_num == 3:  # Subfield # 4: IAS Indicated Airspeed / Mach No 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubIAS = 4
                            #if 'IAS_kt' in Received.ContentSiftFromCat062 or 'IAS_mach' in Received.ContentSiftFromCat062:
                            Subfield_IAS_hex = data_str[
                                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG]
                            Subfield_IAS_bin = hex2bin(Subfield_IAS_hex)
                            if Subfield_IAS_bin[0] == "0":
                                Subfield_IAS_kt = bin2int(Subfield_IAS_bin[1:]) * 0.2197265625
                                AircraftDerivedData.update({'IAS_kt': Subfield_IAS_kt})
                            else:
                                Subfield_IAS_mach = bin2int(Subfield_IAS_bin[1:]) * 0.001
                                AircraftDerivedData.update({'IAS_mach': Subfield_IAS_mach})

                    elif bit_num == 4:  # Subfield # 5: TAS True Airspeed 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubTAS = 4
                            #if 'TAS_kt' in Received.ContentSiftFromCat062:
                            Subfield_TAS = hex2int(data_str[
                                                       x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS])
                            AircraftDerivedData.update({'TAS_kt': Subfield_TAS})

                    elif bit_num == 5:  # Subfield # 6: SAL Selected Altitude 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubSAL = 4
                            #if 'SAL_m_FCU/MCO' in Received.ContentSiftFromCat062:
                            Subfield_SAL_hex = hex2int(data_str[
                                                           x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS])
                            Subfield_SAL_bin = hex2bin(Subfield_SAL_hex)
                            Subfield_SAL = bin2int(Subfield_SAL_bin[3:]) * 25 * 0.3048
                            if Subfield_SAL_bin[1:3] == "00":
                                AircraftDerivedData.update({'SAL_m_unknown': Subfield_SAL})
                            elif Subfield_SAL_bin[1:3] == "01":
                                AircraftDerivedData.update({'SAL_m_AircraftAlititude': Subfield_SAL})
                            elif Subfield_SAL_bin[1:3] == "10":
                                AircraftDerivedData.update({'SAL_m_FCU/MCO': Subfield_SAL})
                            elif Subfield_SAL_bin[1:3] == "11":
                                AircraftDerivedData.update({'SAL_m_FMS': Subfield_SAL})

                    elif bit_num == 6:  # Subfield # 7: FSS FinalStateSelectedAltitude -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubFSS = 4
                            Subfield_FSS_bin = hex2bin(data_str[
                                                       x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL])
                            #if 'FSS' in Received.ContentSiftFromCat062:
                            if Subfield_FSS_bin[0] == "0":
                                Subfield_FSS = bin2int(Subfield_FSS_bin[3:]) * 25 * 0.3048
                            else:
                                Subfield_FSS = (bin2int(Subfield_FSS_bin[4:]) - 4096) * 25 * 0.3048

                            AircraftDerivedData.update({'FSS': Subfield_FSS})

                    elif bit_num == 8:  # Subfield # 8: TIS TrajectoryIntentStatus -- 1+ octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Subfield_TIS_list = JudgeFSPEC(data_str,
                                                           x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS,
                                                           x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS)
                            # print('ashdyt:',Subfield_TIS_list)
                            Len062_380_SubTIS = len(Subfield_TIS_list[0])

                    elif bit_num == 9:  # Subfield # 9: TID TrajectoryIntentData -- 1+ 15+octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Subfield_TID_REP = hex2int(data_str[
                                                       x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS:x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS])
                            Len062_380_SubTID_extend = Subfield_TID_REP * 30
                            Len062_380_SubTID = Len062_380_SubTID_extend + 2

                    elif bit_num == 10:  # Subfield # 10: COM Communications/ACAS Capability and Flight Status reported by Mode-S -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubCOM = 4

                    elif bit_num == 11:  # Subfield # 11: SAB StatusReportedByADS-B -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubSAB = 4

                    elif bit_num == 12:  # Subfield # 12: ACS ACASResolutionAdvisoryReport -- 7 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubACS = 14

                    elif bit_num == 13:  # Subfield # 13: BVR BarometricVerticalRate -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubBVR = 4

                    elif bit_num == 14:  # Subfield # 14: GVR GeometricVerticalRate -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubGVR = 4

                    elif bit_num == 16:  # Subfield # 15: RAN RollAngle -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubRAN = 4

                    elif bit_num == 17:  # Subfield # 16: TAR TrackAngleRate -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubTAR = 4

                    elif bit_num == 18:  # Subfield # 17: TAN  TrackAngle -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubTAN = 4

                    elif bit_num == 19:  # Subfield # 18 GSP  GroundSpeed -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubGSP = 4

                    elif bit_num == 20:  # Subfield # 19 VUN  Velocity Uncertainty -- 1 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubVUN = 2

                    elif bit_num == 21:  # Subfield # 20 MET  Met Data -- 8 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubMET = 16

                    elif bit_num == 22:  # Subfield # 21 EMC  Emitter Category -- 1 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubEMC = 2

                    elif bit_num == 24:  # Subfield # 22 POS  Position -- 6 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubPOS = 12

                    elif bit_num == 25:  # Subfield # 23 GAL  Geometric Altitude -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubGAL = 4

                    elif bit_num == 26:  # Subfield # 24 PUN  Position Uncertainty -- 1 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubPUN = 2

                    elif bit_num == 27:  # Subfield # 25 MB  MODESMBDATA -- 1+ 8*+ octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Subfield_MB_REP = hex2int(data_str[
                                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS + Len062_380_SubTID + Len062_380_SubCOM + Len062_380_SubSAB + Len062_380_SubACS + Len062_380_SubBVR + Len062_380_SubGVR + Len062_380_SubRAN + Len062_380_SubTAR + Len062_380_SubTAN + Len062_380_SubGSP + Len062_380_SubVUN + Len062_380_SubMET + Len062_380_SubEMC + Len062_380_SubPOS + Len062_380_SubGAL + Len062_380_SubPUN:x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS + Len062_380_SubTID + Len062_380_SubCOM + Len062_380_SubSAB + Len062_380_SubACS + Len062_380_SubBVR + Len062_380_SubGVR + Len062_380_SubRAN + Len062_380_SubTAR + Len062_380_SubTAN + Len062_380_SubGSP + Len062_380_SubVUN + Len062_380_SubMET + Len062_380_SubEMC + Len062_380_SubPOS + Len062_380_SubGAL + Len062_380_SubPUN])
                            Len062_380_SubMB_extend = Subfield_MB_REP * 16
                            Len062_380_SubMB = Len062_380_SubMB_extend + 2

                    elif bit_num == 28:  # Subfield # 26 IAR  Indicated Airspeed -- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubIAR = 4

                    elif bit_num == 29:  # Subfield # 27 MAC  Mach Number-- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubMAC = 4

                    elif bit_num == 30:  # Subfield # 28 BPS  BarometricPressureSetting-- 2 octet
                        if Item380Fspec_bin[bit_num] == "1":
                            Len062_380_SubBPS = 4
                            #if 'BarometricPressureSetting' in Received.ContentSiftFromCat062:
                            Subfield_BPS_hex = data_str[
                                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS + Len062_380_SubTID + Len062_380_SubCOM + Len062_380_SubSAB + Len062_380_SubACS + Len062_380_SubBVR + Len062_380_SubGVR + Len062_380_SubRAN + Len062_380_SubTAR + Len062_380_SubTAN + Len062_380_SubGSP + Len062_380_SubVUN + Len062_380_SubMET + Len062_380_SubEMC + Len062_380_SubPOS + Len062_380_SubGAL + Len062_380_SubPUN + Len062_380_SubMB + Len062_380_SubIAR + Len062_380_SubMAC:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS + Len062_380_SubTID + Len062_380_SubCOM + Len062_380_SubSAB + Len062_380_SubACS + Len062_380_SubBVR + Len062_380_SubGVR + Len062_380_SubRAN + Len062_380_SubTAR + Len062_380_SubTAN + Len062_380_SubGSP + Len062_380_SubVUN + Len062_380_SubMET + Len062_380_SubEMC + Len062_380_SubPOS + Len062_380_SubGAL + Len062_380_SubPUN + Len062_380_SubMB + Len062_380_SubIAR + Len062_380_SubMAC]
                            Subfield_BPS_bin = hex2bin(Subfield_BPS_hex)
                            BarometricPressureSetting = bin2int(Subfield_BPS_bin[4:]) + 800
                            AircraftDerivedData.update({'BarometricPressureSetting': BarometricPressureSetting})

                Len062_380 = Len062_380_fspec + Len062_380_SubADR + Len062_380_SubID + Len062_380_SubMHG + Len062_380_SubIAS + Len062_380_SubTAS + Len062_380_SubSAL + Len062_380_SubFSS + Len062_380_SubTIS + Len062_380_SubTID + Len062_380_SubCOM + Len062_380_SubSAB + Len062_380_SubACS + Len062_380_SubBVR + Len062_380_SubGVR + Len062_380_SubRAN + Len062_380_SubTAR + Len062_380_SubTAN + Len062_380_SubGSP + Len062_380_SubVUN + Len062_380_SubMET + Len062_380_SubEMC + Len062_380_SubPOS + Len062_380_SubGAL + Len062_380_SubPUN + Len062_380_SubMB + Len062_380_SubIAR + Len062_380_SubMAC + Len062_380_SubBPS
                #if 'Item380' in Received.ContentSiftFromCat062:
                InfoDecodeInCat062_one.update({'Item380': AircraftDerivedData})

        elif bit_i == 12:  # /040 TrackNumber -- 2 octets
            if fspec_062_bin[bit_i] == "1":
                Len062_040 = 4
                #if 'TN' in Received.ContentSiftFromCat062:
                I062_040 = hex2int(data_str[
                                       x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380])
                InfoDecodeInCat062_one.update({'TN': I062_040})
            # print(InfoDecodeInCat062_one)

        elif bit_i == 13:  # /080 TrackStatus -- 1+ octets
            if fspec_062_bin[bit_i] == "1":
                I062_080 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040)
                # print('/080:',I062_080)
                Len062_080 = len(I062_080[0])
                #if 'SIM' in Received.ContentSiftFromCat062:
                if Len062_080 > 2:
                    if I062_080[1][8] == "1":
                        InfoDecodeInCat062_one.update({'SIM': 'Simulated'})
                    else:
                        InfoDecodeInCat062_one.update({'SIM': 'Actual'})

        elif bit_i == 14:  # /290 System TrackUpdateAges -- 1+ octets
            if fspec_062_bin[bit_i] == "1":
                I062_290 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080)
                Len062_290_Primary = len(I062_290[0])
                I062_290_bin = I062_290[1]
                Len_I062_290_Sub_ini, Len_I062_290_Sub = 0, 0
                for bit290 in range(len(I062_290_bin)):
                    if bit290 == 4:
                        if I062_290_bin[bit290] == "1":
                            Len_I062_290_Sub = 4

                    elif bit290 == 7:
                        Len_I062_290_Sub = 0
                    else:
                        if I062_290_bin[bit290] == "1":
                            Len_I062_290_Sub = 2

                    Len_I062_290_Sub_ini += Len_I062_290_Sub
                Len062_290 = Len_I062_290_Sub_ini + Len062_290_Primary

        elif bit_i == 16:  # FRN 15 /200 ModeOfMovement  --1 octet
            if fspec_062_bin[bit_i] == "1":
                Len062_200 = 2

        elif bit_i == 17:  # FRN 16  /295 Track Data Ages  --1+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_295 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200)
                Len062_295_Primary = len(I062_295[0])
                I062_295_bin = I062_295[1]
                Len_I062_295_Sub_ini, Len_I062_295_Sub = 0, 0
                for bit295 in range(len(I062_295_bin)):
                    if bit295 == 7 or bit295 == 15 or bit295 == 23 or bit295 == 31 or bit295 == 39:
                        Len_I062_295_Sub = 0
                    else:
                        if I062_295_bin[bit295] == "1":
                            Len_I062_295_Sub = 2
                    Len_I062_295_Sub_ini += Len_I062_295_Sub
                Len062_295 = Len_I062_295_Sub_ini + Len062_295_Primary

        elif bit_i == 18:  # FRN 17 /136 MeasuredFlightLevel  --2 octet this item includesbarometic altitude received from ADS-B
            if fspec_062_bin[bit_i] == "1":
                Len062_136 = 4
                #if 'MFL_m' in Received.ContentSiftFromCat062:
                I062_136_bin = hex2bin(data_str[
                                           x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295])
                if I062_136_bin[0] == "0":
                    I062_136 = bin2int(I062_136_bin) * 25 * 0.3048
                else:
                    I062_136 = (bin2int(I062_136_bin[1:]) - 32768) * 25 * 0.3048

                I062_136 = int(Decimal(str(I062_136)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))

                InfoDecodeInCat062_one.update({'MFL_m': I062_136})

        elif bit_i == 19:  # FRN 18 /130 CalculatedTrackGeometricAltitude  --2 octet
            if fspec_062_bin[bit_i] == "1":
                Len062_130 = 4
                #if 'CalculatedGeometricAltitude_m' in Received.ContentSiftFromCat062:
                I062_130_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136]

                I062_130 = hex2int(I062_130_hex) * 6.25 * 0.3048

                I062_130 = int(Decimal(str(I062_130)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))
                InfoDecodeInCat062_one.update({'CalculatedGeometricAltitude_m': I062_130})

        elif bit_i == 20:  # FRN 19 /135 CalculatedTrackBarometricAltitude  --2 octet
            if fspec_062_bin[bit_i] == "1":
                Len062_135 = 4
                #if 'CalculatedBarometricAltitude_m' in Received.ContentSiftFromCat062:
                I062_135_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130]
                #print('I62_135_hex:',I062_135_hex)
                I062_135_bin = hex2bin(I062_135_hex)
                I062_135 = bin2int(I062_135_bin[1:]) * 25 * 0.3048
                I062_135 = int(Decimal(str(I062_135)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))

                if I062_135_bin[0] == '0':
                    InfoDecodeInCat062_one.update({'CalculatedBarometricAltitude_m': {'QNE':I062_135}})

                elif I062_135_bin[0] == '1':
                    InfoDecodeInCat062_one.update({'CalculatedBarometricAltitude_m': {'QNH':I062_135}})

        elif bit_i == 21:  # FRN 20 /220 Calculated Rate Of Climb/Descent  --2 octet
            if fspec_062_bin[bit_i] == "1":
                Len062_220 = 4
                #if 'RateOfClimbOrDescent_ft/m' in Received.ContentSiftFromCat062:
                I062_220_hex = data_str[
                                   x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135]
                #补码
                I062_220_bin = hex2bin(I062_220_hex)
                if I062_220_bin[0] == "0":
                    I062_220 = bin2int(I062_220_bin) * 6.25
                else:
                    I062_220 = (bin2int(I062_220_bin[1:]) - 32768) * 6.25

                I062_220 = int(Decimal(str(I062_220)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))



                #I062_220 = hex2int(I062_220_hex) * 6.25

                #I062_220 = int(Decimal(str(I062_220)).quantize(Decimal('0'), rounding=ROUND_HALF_UP))
                InfoDecodeInCat062_one.update({'RateOfClimbOrDescent_ft/m': I062_220})

        elif bit_i == 22:  # FRN 21 /390 FlightPlanRelatedData  --1+ octet
            if fspec_062_bin[bit_i] == "1":
                InfoItem390 = {}
                Item390Fspec = JudgeFSPEC(data_str,
                                          x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220,
                                          x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220)
                # Item390Fspec_hex = Item390Fspec[0]
                Len062_390_fspec = len(Item390Fspec[0])
                Item390Fspec_bin = Item390Fspec[1]
                for bit390 in range(len(Item390Fspec_bin)):
                    # 0    1   2   3   4   5   6   7  8   9  10  11  12  13  14  15 16  17  18  19  20  21  22  23
                    # TAG CSN IFI FCT TAC WTC DEP FX DST RDS CFL CTL TOD AST STS FX STD STA PEM PEC  0   0   0  FX
                    if bit390 == 0:  # FRN1 /Subfield 1: TAG390 FPPSIdentificationTag  | SAC_SIC --2 octet
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubTAG = 4


                    elif bit390 == 1:  # FRN 2 /Subfield 2  CSN390 Callsign  -- 7 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubCSN = 14
                            Item390_Sub_CSN_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG:x + 14 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG]
                            Item390_Sub_CSN_hex_list = wrap(Item390_Sub_CSN_hex, 2)
                            Item390_Sub_CSN_list = ''
                            for CSN_ones in Item390_Sub_CSN_hex_list:
                                CSN_ones_chr = chr(hex2int(CSN_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_CSN_list += CSN_ones_chr
                            InfoItem390.update({"Callsign": Item390_Sub_CSN_list})


                    elif bit390 == 2:  # FRN 3 /Subfield 3 IFI390 IFPS_FLIGHT_ID  -- 4 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubIFI = 8


                    elif bit390 == 3:  # FRN 4 /Subfield 4 FCT390 Flight Category  -- 1 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubFCT = 2


                    elif bit390 == 4:  # FRN 5 /Subfield 5 TAC390 Type of Aircraft  -- 4 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubTAC = 8
                            Item390_Sub_TAC_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT]
                            Item390_Sub_TAC_hex_list = wrap(Item390_Sub_TAC_hex, 2)
                            Item390_Sub_TAC_list = ''
                            for TAC_ones in Item390_Sub_TAC_hex_list:
                                TAC_ones_chr = chr(hex2int(TAC_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_TAC_list += TAC_ones_chr
                            InfoItem390.update({"AircraftType": Item390_Sub_TAC_list})


                    elif bit390 == 5:  # FRN 6 /Subfield 6 WTC390 Wake Turbulence Category  -- 1 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubWTC = 2
                            Item390_Sub_WTC_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC:x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC]
                            Item390_Sub_WTC = chr(hex2int(Item390_Sub_WTC_hex))
                            InfoItem390.update({"WTC": Item390_Sub_WTC})


                    elif bit390 == 6:  # FRN 7 /Subfield 7 DEP390 Departure Airport  -- 4 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubDEP = 8
                            Item390_Sub_DEP_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC]
                            Item390_Sub_DEP_hex_list = wrap(Item390_Sub_DEP_hex, 2)
                            Item390_Sub_DEP_list = ''
                            for DEP_ones in Item390_Sub_DEP_hex_list:
                                DEP_ones_chr = chr(hex2int(DEP_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_DEP_list += DEP_ones_chr
                            InfoItem390.update({"DEP": Item390_Sub_DEP_list})


                    elif bit390 == 8:  # FRN 8 /Subfield 8 DST390 Destination Airport  -- 4 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubDST = 8
                            Item390_Sub_DST_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP:x + 8 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP]
                            Item390_Sub_DST_hex_list = wrap(Item390_Sub_DST_hex, 2)
                            Item390_Sub_DST_list = ''
                            for DST_ones in Item390_Sub_DST_hex_list:
                                DST_ones_chr = chr(hex2int(DST_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_DST_list += DST_ones_chr
                            InfoItem390.update({"DST": Item390_Sub_DST_list})


                    elif bit390 == 9:  # FRN 9 /Subfield 9 RDS390 Destination Airport  -- 3 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubRDS = 6
                            Item390_Sub_RDS_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST:x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST]
                            Item390_Sub_RDS_hex_list = wrap(Item390_Sub_RDS_hex, 2)
                            Item390_Sub_RDS_list = ''
                            for RDS_ones in Item390_Sub_RDS_hex_list:
                                RDS_ones_chr = chr(hex2int(RDS_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_RDS_list += RDS_ones_chr
                            InfoItem390.update({"RWY": Item390_Sub_RDS_list})


                    elif bit390 == 10:  # FRN 10 /Subfield 10 CFL390 Current Cleared Flight Level  -- 2 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubCFL = 4
                            Item390_Sub_CFL_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS]
                            Item390_Sub_CFL = hex2int(Item390_Sub_CFL_hex) * 25 * 0.3048
                            InfoItem390.update({"CFL_m": Item390_Sub_CFL})


                    elif bit390 == 11:  # FRN 11 /Subfield 11 CTL390 Current Control Position  -- 2 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubCTL = 4
                            Item390_Sub_CTL_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL:x + 4 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL]
                            Item390_Sub_CTL_hex_list = wrap(Item390_Sub_CTL_hex, 2)
                            Item390_Sub_CTL_list = ''
                            for CTL_ones in Item390_Sub_CTL_hex_list:
                                CTL_ones_chr = chr(hex2int(CTL_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_CTL_list += CTL_ones_chr
                            # print(Item390_Sub_CTL_list)
                            InfoItem390.update({"ControlPosition": Item390_Sub_CTL_list})


                    elif bit390 == 12:  # FRN 12 /Subfield 12 TOD390 Time of Departure / Arrival  -- 1+4+ octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubTOD_REP = 2
                            Item390_Sub_TOD_REP = hex2int(data_str[
                                                          x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL:x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL])
                            Len390_Sub_TOD_SUB = 8 * Item390_Sub_TOD_REP
                            Len062_390_SubTOD = Len390_Sub_TOD_SUB + Len062_390_SubTOD_REP


                    elif bit390 == 13:  # FRN 13 /Subfield 13 AST390 Aircraft Stand  -- 6 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubAST = 12


                    elif bit390 == 14:  # FRN 14 /Subfield 14 STS390 Stand Status  -- 1 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubSTS = 2


                    elif bit390 == 16:  # FRN 15 /Subfield 15 STD390 Standard Instrument Departure  -- 7 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubSTD = 14
                            Item390_Sub_STD_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL + Len062_390_SubTOD + Len062_390_SubAST + Len062_390_SubSTS:x + 14 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL + Len062_390_SubTOD + Len062_390_SubAST + Len062_390_SubSTS]
                            Item390_Sub_STD_hex_list = wrap(Item390_Sub_STD_hex, 2)
                            Item390_Sub_STD_list = ""
                            for STD_ones in Item390_Sub_STD_hex_list:
                                STD_ones_chr = chr(hex2int(STD_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_STD_list += STD_ones_chr
                            InfoItem390.update({"SID": Item390_Sub_STD_list})


                    elif bit390 == 17:  # FRN 16 /Subfield 16 STA390 Standard Instrument Arrival  -- 7 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubSTA = 14
                            Item390_Sub_STA_hex = data_str[
                                                  x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL + Len062_390_SubTOD + Len062_390_SubAST + Len062_390_SubSTS + Len062_390_SubSTD:x + 14 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL + Len062_390_SubTOD + Len062_390_SubAST + Len062_390_SubSTS + Len062_390_SubSTD]
                            Item390_Sub_STA_hex_list = wrap(Item390_Sub_STA_hex, 2)
                            Item390_Sub_STA_list = ""
                            for STA_ones in Item390_Sub_STA_hex_list:
                                STA_ones_chr = chr(hex2int(STA_ones))
                                # print(CSN_ones_chr)
                                Item390_Sub_STA_list += STA_ones_chr
                            InfoItem390.update({"STAR": Item390_Sub_STA_list})


                    elif bit390 == 18:  # FRN 17 /Subfield 17 PEM390 Pre-Emergency Mode 3/A  -- 2 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubPEM = 4


                    elif bit390 == 19:  # FRN 18 /Subfield 18 PEC390 Pre-Emergency Callsign  -- 7 octets
                        if Item390Fspec_bin[bit390] == "1":
                            Len062_390_SubPEC = 14

                Len062_390 = Len062_390_fspec + Len062_390_SubTAG + Len062_390_SubCSN + Len062_390_SubIFI + Len062_390_SubFCT + Len062_390_SubTAC + Len062_390_SubWTC + Len062_390_SubDEP + Len062_390_SubDST + Len062_390_SubRDS + Len062_390_SubCFL + Len062_390_SubCTL + Len062_390_SubTOD + Len062_390_SubAST + Len062_390_SubSTS + Len062_390_SubSTD + Len062_390_SubSTA + Len062_390_SubPEM + Len062_390_SubPEC
                #if 'Item390' in Received.ContentSiftFromCat062:
                InfoDecodeInCat062_one.update({'Item390': InfoItem390})

        elif bit_i == 24:  # FRN 22 /270 Target Size & Orientation --1+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_270 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390)
                Len062_270 = len(I062_270[0])

        elif bit_i == 25:  # FRN 23 /300 Vehicle Fleet Identification --1 octet
            if fspec_062_bin[bit_i] == "1":
                # I062_270 = JudgeFSPEC(data_str,x+ LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080+Len062_290+Len062_200+Len062_295+Len062_136+Len062_130+Len062_135+Len062_220+Len062_390,x+2+ LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080+Len062_290+Len062_200+Len062_295+Len062_136+Len062_130+Len062_135+Len062_220+Len062_390)
                Len062_300 = 2

        elif bit_i == 26:  # FRN 24 /110 Mode 5 Data reports & Extended Mode 1 Code --1+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_110 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300)
                len_Item110_fspec = len(I062_110[0])
                Item110_fspec_bin = I062_110[1]
                # Item110_fspec_bin_list = wrap(Item110_fspec_bin,8)
                Len062_110_sub = 0
                # for item_110 in Item110_fspec_bin_list:
                for bit110 in range(len(Item110_fspec_bin)):
                    #  0   1   2  3  4   5   6  7
                    # SUM PMN POS GA EM1 TOS XP FX
                    if bit110 == 0:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_SUM = 2


                    elif bit110 == 1:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_PMN = 8


                    elif bit110 == 2:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_POS = 12


                    elif bit110 == 3:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_GA = 4


                    elif bit110 == 4:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_EM1 = 4


                    elif bit110 == 5:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_TOS = 2


                    elif bit110 == 6:
                        if Item110_fspec_bin[bit110] == "1":
                            Len062_110_Sub_XP = 2

                Len062_110_sub = Len062_110_Sub_SUM + Len062_110_Sub_PMN + Len062_110_Sub_POS + Len062_110_Sub_GA + Len062_110_Sub_EM1 + Len062_110_Sub_TOS + Len062_110_Sub_XP
                Len062_110 = len_Item110_fspec + Len062_110_sub

        elif bit_i == 27:  # FRN 25 /120 Track Mode 2 Code --2 octet
            if fspec_062_bin[bit_i] == "1":
                # I062_270 = JudgeFSPEC(data_str,x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080+Len062_290+Len062_200+Len062_295+Len062_136+Len062_130+Len062_135+Len062_220+Len062_390,x+2+ LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080+Len062_290+Len062_200+Len062_295+Len062_136+Len062_130+Len062_135+Len062_220+Len062_390)
                Len062_120 = 4

        elif bit_i == 28:  # FRN 26 /510 Composed Track Number --3+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_510 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120,
                                      x + 6 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120)
                Len062_510 = len(I062_510[0])

        elif bit_i == 29:  # FRN 27 /500 Estimated Accuracies --1+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_500 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120 + Len062_510,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120 + Len062_510)
                Len062_500_fspec = len(I062_500[0])
                I062_500_bin = I062_500[1]
                Len_I062_500_Sub_ini = 0
                for bit500 in range(len(I062_500_bin)):
                    if bit500 == 0 or bit500 == 2:
                        if I062_500_bin[bit500] == "1":
                            Len_I062_500_Sub = 8

                    elif bit500 == 7:
                        Len_I062_500_Sub = 0
                    elif bit500 == 1 or bit500 == 5 or bit500 == 6:
                        if I062_500_bin[bit500] == "1":
                            Len_I062_500_Sub = 4


                        else:
                            if I062_500_bin[bit500] == "1":
                                Len_I062_500_Sub = 2

                    Len_I062_500_Sub_ini += Len_I062_500_Sub
                Len062_500 = Len_I062_500_Sub_ini + Len062_500_fspec

        elif bit_i == 30:  # FRN 28 /340 Measured Information --1+ octet
            if fspec_062_bin[bit_i] == "1":
                I062_340 = JudgeFSPEC(data_str,
                                      x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120 + Len062_510 + Len062_500,
                                      x + 2 + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120 + Len062_510 + Len062_500)
                Len062_340_fspec = len(I062_340[0])
                I062_340_bin = I062_340[1]
                Len_I062_340_Sub_ini, Len_I062_340_Sub = 0, 0
                for bit340 in range(len(I062_340_bin)):
                    if bit340 == 1:
                        if I062_340_bin[bit340] == "1":
                            Len_I062_340_Sub = 8

                    elif bit340 == 7:
                        Len_I062_340_Sub = 0
                    elif bit340 == 5:
                        if I062_340_bin[bit340] == "1":
                            Len_I062_340_Sub = 2

                    else:
                        if I062_340_bin[bit340] == "1":
                            Len_I062_340_Sub = 2

                    Len_I062_340_Sub_ini += Len_I062_340_Sub
                Len062_340 = Len_I062_340_Sub_ini + Len062_340_fspec

    Len_SectionOne = x + LenFspecHex_062 + Len062_010 + Len062_015 + Len062_070 + Len062_105 + Len062_100 + Len062_185 + Len062_210 + Len062_060 + Len062_245 + Len062_380 + Len062_040 + Len062_080 + Len062_290 + Len062_200 + Len062_295 + Len062_136 + Len062_130 + Len062_135 + Len062_220 + Len062_390 + Len062_270 + Len062_300 + Len062_110 + Len062_120 + Len062_510 + Len062_500 + Len062_340

    return (InfoDecodeInCat062_one, Len_SectionOne)

def Category048_track(data, x):
    data_str_048 = data
    # InfoDecodeInCat048 = {}
    InfoDecodeInCat048_one = {}
    # print ('读入的数据：',self.data)
    # global Len_140, Len_010, Len_020, Len_040, Len_070, Len_090, Len_130, Len_220, Len_240, Len_250, Len_161, Len_042, Len_200
    Len_140, Len_010, Len_020, Len_040, Len_070, Len_090, Len_130, Len_220, Len_240, Len_250, Len_161, Len_042, Len_200 = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    Len_210, Len_170, Len_030, Len_080, Len_100, Len_110, Len_120, Len_230, Len_260, Len_055, Len_050, Len_065, Len_060, Len_SectionOne = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    InfoDecodeInCat048 = {}

    fspec_048 = JudgeFSPEC(data_str_048, x, x + 2)
    # print ('FSPEC:',fspec_048)
    fspec_048_hex = fspec_048[0]
    fspec_048_bin = fspec_048[1]
    LenFspecHex_048 = len(fspec_048_hex)

    # if self.JudgeDataLength() == True:
    # fspec_048 = JudgeFSPEC(self.data, 6, 8)
    # print ('FSPEC:',fspec_048)
    # fspec_048_hex = fspec_048[0]
    # fspec_048_bin = fspec_048[1]
    # LenFspecHex = len(fspec_048_hex)
    for i in range(len(fspec_048_bin)):
        if i == 0:
            if fspec_048_bin[i] == "1":
                # print('FRN1:',self.data[6 + LenFspecHex:10 + LenFspecHex])
                I048_010_DataSourceIdentifier = data_str_048[x + LenFspecHex_048:x + 4 + LenFspecHex_048]
                Len_010 = 4
                InfoDecodeInCat048.update({'SAC_SIC': I048_010_DataSourceIdentifier})

            # print('FRN1:',InfoDecodeInCat048)

        elif i == 1:
            if fspec_048_bin[i] == "1":
                I048_140_hex = data_str_048[x + LenFspecHex_048 + Len_010:x + 6 + LenFspecHex_048 + Len_010]
                # print ('FRN2:',I048_140_hex)
                '''I048_140_hour = int(hex2int(I048_140_hex) / 128 / 3600)
                I048_140_min = int((hex2int(I048_140_hex) / 128 / 3600 - I048_140_hour) * 60)
                I048_140_second = ((hex2int(I048_140_hex) / 128 / 3600 - I048_140_hour) * 60 - I048_140_min) * 60
                I048_140_TimeOfDay = '%s:%s:%s' % (I048_140_hour, I048_140_min, I048_140_second)'''
                Len_140 = 6
                #InfoDecodeInCat048.update({'TimeOfDay': I048_140_TimeOfDay})

        # print('FRN2:', InfoDecodeInCat048)

        elif i == 2:
            if fspec_048_bin[i] == "1":
                I048_020 = JudgeFSPEC(data_str_048, x + LenFspecHex_048 + Len_010 + Len_140,
                                      x + 2 + LenFspecHex_048 + Len_010 + Len_140)
                # print('FRN3:', I048_020)
                Len_020 = len(I048_020[0])
                I048_020_bin = I048_020[1]

                I048_020_bin_list = wrap(I048_020_bin, 8)
                for Octet_no in range(len(I048_020_bin_list)):
                    if Octet_no == 0:
                        '''TYP = ''
                        if I048_020_bin_list[0][0:3] == "000":
                            TYP = "NoDetection"
                        elif I048_020_bin_list[0][0:3] == "001":
                            TYP = "SinglePSR"
                        elif I048_020_bin_list[0][0:3] == "010":
                            TYP = "SingleSSR"
                        elif I048_020_bin_list[0][0:3] == "011":
                            TYP = "SSR&PSR"
                        elif I048_020_bin_list[0][0:3] == "100":
                            TYP = "SingleModeSAllCall"
                        elif I048_020_bin_list[0][0:3] == "101":
                            TYP = "SingleModeSRollCall"
                        elif I048_020_bin_list[0][0:3] == "110":
                            TYP = "ModeSAllCall&PSR"
                        elif I048_020_bin_list[0][0:3] == "111":
                            TYP = "ModeSRollCall&PSR"
                        InfoDecodeInCat048.update({'TYP': TYP})'''

                        if I048_020_bin_list[0][3] == "0":
                            InfoDecodeInCat048.update({'SIM': "Actual"})
                        else:
                            InfoDecodeInCat048.update({'SIM': "Simulated"})
                    else:
                        if I048_020_bin_list[Octet_no][0] == "0":
                            InfoDecodeInCat048.update({'TST': "Real"})
                        else:
                            InfoDecodeInCat048.update({'TST': "TEST"})

        # print ('ss:',InfoDecodeInCat048)

        elif i == 3:
            if fspec_048_bin[i] == "1":
                '''I048_040_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020:x + 8 + LenFspecHex_048 + Len_010 + Len_140 + Len_020]
                # print ('FRN4:',I048_040_hex)
                RHO = hex2int(I048_040_hex[0:4]) / 256
                THETA = hex2int(I048_040_hex[4:]) * 360 / 65536'''
                Len_040 = 8
                #InfoDecodeInCat048.update({'PolarCoordinates': {"RHO_NM": RHO, "THETA_Degree": THETA}})


        elif i == 4:
            if fspec_048_bin[i] == "1":
                I048_070_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040:x + 4 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040]
                # print ('FRN5:',I048_070_hex)
                I048_070_bin = hex2bin(I048_070_hex)
                # print('I048_070_bin:',I048_070_bin)
                I048_070_Mode3ACode = ''

                if I048_070_bin[0] == "0" and I048_070_bin[1] == '0':
                    I048_070_Mode3ACode_bin = wrap(I048_070_bin[4:], 3)
                    for ii in I048_070_Mode3ACode_bin:
                        i_oct = bin2oct(ii)
                        I048_070_Mode3ACode += i_oct

                    InfoDecodeInCat048.update({'Mode3ACode': I048_070_Mode3ACode})
                else:
                    InfoDecodeInCat048.update({'Mode3ACode': 'NotUseable'})
                Len_070 = 4


        elif i == 5:
            if fspec_048_bin[i] == "1":
                I048_090_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070:x + 4 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070]
                # print ('FRN6:',I048_090_hex)
                Len_090 = 4
                I048_090_bin = hex2bin(I048_090_hex)
                if I048_090_bin[0] == "0":
                    if I048_090_bin[1] == "0":
                        # FlightLevel_ft = int(bin2int(I048_090_bin[2:]) * 25)
                        if I048_090_bin[2] == "0":
                            FlightLevel_m = str(int((bin2int(I048_090_bin[2:]) * 25 * 0.3048)))
                        else:
                            FlightLevel_m = str(int((bin2int(I048_090_bin[2:]) - 8192) * 25 * 0.3048))
                    else:
                        # FlightLevel_ft = "NULL"
                        FlightLevel_m = "NULL"
                else:
                    # FlightLevel_ft = "NULL"
                    FlightLevel_m = "NULL"
                InfoDecodeInCat048.update({'FlightLevel_m': FlightLevel_m})


        elif i == 6:
            if fspec_048_bin[i] == "1":
                # FRN 7 I048/130 Radar Plot Characterisitics
                I048_130 = JudgeFSPEC(data_str_048,
                                      x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090,
                                      x + 2 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090)
                # print ('FRN7:',I048_130[0])
                I048_130_bin = I048_130[1]
                #				print('/130:',I048_130,I048_130_bin)
                I048_130_bin_list = wrap(I048_130_bin, 8)
                len_I048_130_bin_list = len(I048_130_bin_list)
                #				print(len_I048_130_bin_list)
                I048_130_Subfield_Num_initial = 0
                for ii in I048_130_bin_list:
                    for ii_is_1 in ii:
                        if ii_is_1 == "1":
                            I048_130_Subfield_Num_initial += 1
                I048_130_Subfield_Num = I048_130_Subfield_Num_initial - (len_I048_130_bin_list - 1)
                LenSubfield_130 = I048_130_Subfield_Num * 2
                Len_130 = len(I048_130[0]) + LenSubfield_130
        #				print(Len_130)

        elif i == 8:
            if fspec_048_bin[i] == "1":
                AircraftAdress = data_str_048[
                                 x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130:x + 6 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130]
                # print ('FRN8:',AircraftAdress)
                Len_220 = 6
                InfoDecodeInCat048.update({'AircraftAdress': AircraftAdress})


        elif i == 9:
            if fspec_048_bin[i] == "1":
                CallSign_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220:x + 12 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220]
                # print('FRN9:',CallSign_hex)
                Len_240 = 12
                CallSign = callsign(CallSign_hex)
                # CallSign_2 = callsign_2(CallSign_hex)
                # print ('测试：',CallSign_2)
                InfoDecodeInCat048.update({'CallSign': CallSign})

        # print ('pp:',InfoDecodeInCat048)

        elif i == 10:
            if fspec_048_bin[i] == "1":
                ModeSMBData = {}
                REP_hex = data_str_048[
                          x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240:x + 2 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240]
                REP_int = hex2int(REP_hex)
                # print('rep-int:',REP_int)
                Len_250_Subfield = 16 * REP_int
                # I048_250_hex = self.data[6 + LenFspecHex+ Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + 2:6 + LenFspecHex+ Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + 2 + Len_250]
                # 为减少一次rep长度2字符的计算 将上述6加上rep字符长度 ，下面直接写成8
                I048_250_hex = data_str_048[
                               x + 2 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240:x + 2 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250_Subfield]
                # print ('FRN10:',REP_hex,I048_250_hex)
                Len_250 = Len_250_Subfield + 2
                s = wrap(I048_250_hex, 16)
                for REP_sub in s:
                    # print(REP_sub)
                    if REP_sub[-2:] == "40":
                        mode_s_bds40 = BDS40(str(REP_sub[:-2]))
                        ModeSMBData.update(mode_s_bds40)
                    elif REP_sub[-2:] == "50":
                        mode_s_bds50 = BDS50(str(REP_sub[:-2]))
                        ModeSMBData.update(mode_s_bds50)
                    elif REP_sub[-2:] == "60":
                        mode_s_bds60 = BDS60(str(REP_sub[:-2]))
                        ModeSMBData.update(mode_s_bds60)
                    else:
                        ModeSMBData.update({"BDS": 'NULL'})
                    InfoDecodeInCat048.update({'ModeSMBData': ModeSMBData})
            # print('kk',InfoDecodeInCat048)


        elif i == 11:
            if fspec_048_bin[i] == "1":
                Len_161 = 4


        elif i == 12:
            if fspec_048_bin[i] == "1":
                I048_042_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161:x + 8 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161]
                # print('FRN12:',I048_042_hex)
                Len_042 = 8

                '''CartesianCoOrdinatesX_NM_bin = hex2bin(I048_042_hex[0:4])
                CartesianCoOrdinatesY_NM_bin = hex2bin(I048_042_hex[4:])

                if CartesianCoOrdinatesX_NM_bin[0] == '0':
                    CartesianCoOrdinatesX_NM = bin2int(CartesianCoOrdinatesX_NM_bin) / 128
                else:
                    CartesianCoOrdinatesX_NM = (bin2int(CartesianCoOrdinatesX_NM_bin[1:]) - 32768) / 128

                if CartesianCoOrdinatesY_NM_bin[0] == "0":
                    CartesianCoOrdinatesY_NM = bin2int(CartesianCoOrdinatesY_NM_bin) / 128
                else:
                    CartesianCoOrdinatesY_NM = (bin2int(CartesianCoOrdinatesY_NM_bin[1:]) - 32768) / 128

                InfoDecodeInCat048.update(
                    {'CartesianCoOrdinates': {'X_NM': CartesianCoOrdinatesX_NM, 'Y_NM': CartesianCoOrdinatesY_NM}})'''

        elif i == 13:
            if fspec_048_bin[13] == "1":
                I048_200_hex = data_str_048[
                               x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042:x + 8 + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042]
                # print('I048_200_hex:',I048_200_hex)
                # print ('FRN13:',I048_200_hex)
                Len_200 = 8
                GroundSpeed_kt = hex2int(I048_200_hex[0:4]) * 0.2197265625
                Heading_degree = hex2int(I048_200_hex[4:]) * 360 / 65536
                InfoDecodeInCat048.update(
                    {'VelocityInPolar': {'GroundSpeed_kt': GroundSpeed_kt, 'Heading_degree': Heading_degree}})

        # print('asdfg',InfoDecodeInCat048)

        elif i == 14:
            if fspec_048_bin[14] == "1":
                bit_begin = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200
                I048_170 = JudgeFSPEC(data_str_048, bit_begin, bit_begin + 2)
                Len_170 = len(I048_170[0])


        elif i == 16:  # FRN 15 /210 Track Quality --4 octet
            if fspec_048_bin[16] == "1":
                # bit_begin = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200
                Len_210 = 8


        elif i == 17:  # FRN 16 /030 Warning/Error Conditions --1+ octets
            if fspec_048_bin[17] == "1":
                bit_begin_030 = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200 + Len_170 + Len_210
                I048_030 = JudgeFSPEC(data_str_048, bit_begin_030, bit_begin_030 + 2)
                Len_030 = len(I048_030)


        elif i == 18:  # FRN 17 /080 Mode-3/A Code Confidence Indicator --2 octets
            if fspec_048_bin[18] == "1":
                Len_080 = 4

        elif i == 19:  # FRN 18 /100 Mode-C Code and Confidence Indicator --4 octets
            if fspec_048_bin[19] == "1":
                Len_100 = 8

        elif i == 20:  # FRN 19 /110 Height Measured by 3D Radar --2 octets
            if fspec_048_bin[20] == "1":
                Len_110 = 4

        elif i == 21:  # FRN 20 /120 Radial Doppler Speed --1+ octets
            if fspec_048_bin[21] == "1":
                bit_begin_120 = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200 + Len_170 + Len_210 + Len_030 + Len_080 + Len_100 + Len_110
                I048_120_Primary = data_str_048[bit_begin_120, bit_begin_120 + 2]
                I048_120_Primary_bin = hex2bin(I048_120_Primary)
                Len_120_sub1, Len_120_sub2 = 0, 0
                if I048_120_Primary_bin[0] == "1":
                    Len_120_sub1 = 4
                if I048_120_Primary_bin[1] == "1":
                    I048_120_Sub2_REP = hex2int(
                        data_str_048[bit_begin_120 + 2 + Len_120_sub1, bit_begin_120 + 4 + Len_120_sub1])
                    Len_120_Sub2_Extend = I048_120_Sub2_REP * 12
                    Len_120_sub2 = Len_120_Sub2_Extend + 2
                Len_120 = 2 + Len_120_sub1 + Len_120_sub2

        elif i == 22:  # FRN 21 /230 Communications / ACAS Capability and FlightStatus --2 octets
            if fspec_048_bin[22] == "1":
                Len_230 = 4

        elif i == 24:  # FRN 22 /260 ACAS Resolution Advisory Report --7 octets
            if fspec_048_bin[22] == "1":
                bit_begin_260 = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200 + Len_170 + Len_210 + Len_030 + Len_080 + Len_100 + Len_110 + Len_120 + Len_230
                I048_260_hex = data_str_048[bit_begin_260, bit_begin_260 + 14]
                mode_s_bds30 = BDS30(I048_260_hex)
                InfoDecodeInCat048.update(mode_s_bds30)
                Len_260 = 14

        elif i == 25:  # FRN 23 /055 Mode-1 Code in Octal Representation --1 octets
            if fspec_048_bin[25] == "1":
                Len_055 = 2

        elif i == 26:  # FRN 24 /050 Mode-2 Code in Octal Representation --2 octets
            if fspec_048_bin[26] == "1":
                Len_050 = 4

        elif i == 27:  # FRN 25 /065 Mode-1 Code Confidence Indicator --1 octets
            if fspec_048_bin[27] == "1":
                Len_065 = 2

        elif i == 28:  # FRN 26 /060 Mode-2 Code Confidence Indicator --2 octets
            if fspec_048_bin[27] == "1":
                Len_060 = 4
        else:
            pass

        # print('mm',InfoDecodeInCat048)
        Len_SectionOne = x + LenFspecHex_048 + Len_010 + Len_140 + Len_020 + Len_040 + Len_070 + Len_090 + Len_130 + Len_220 + Len_240 + Len_250 + Len_161 + Len_042 + Len_200 + Len_170 + Len_210 + Len_030 + Len_080 + Len_100 + Len_110 + Len_120 + Len_230 + Len_260 + Len_055 + Len_050 + Len_065 + Len_060

    return (InfoDecodeInCat048, Len_SectionOne)

def Category001_pri_track(data, x):
    # global Len001_010,Len001_020
    data001 = data
    # print('分段后子程序收到的字符串：',data001)
    Len001_010, Len001_020 = 0, 0
    InfoDecodeInCat001 = {}
    # if self.JudgeDataLength() == True:
    fspec_001 = JudgeFSPEC(data001, x, x + 2)
    # print('FSPEC:',fspec_001)
    fspec_001_hex = fspec_001[0]
    fspec_001_bin = fspec_001[1]
    LenFspecHex_Cat001 = len(fspec_001_hex)
    Decoded_len = x + LenFspecHex_Cat001
    for i in range(len(fspec_001_bin)):
        if i == 0:
            if fspec_001_bin[i] == "1":
                I001_010_DataSourceIdentifier = data001[x + LenFspecHex_Cat001:x + 4 + LenFspecHex_Cat001]
                Len001_010 = 4
                # print('/010：',I001_010_DataSourceIdentifier)
                InfoDecodeInCat001.update({'SAC_SIC': I001_010_DataSourceIdentifier})
                Decoded_len = x + LenFspecHex_Cat001 + 4

        elif i == 1:
            if fspec_001_bin[i] == "1":
                I001_020 = JudgeFSPEC(data001, x + LenFspecHex_Cat001 + Len001_010,
                                      x + 2 + LenFspecHex_Cat001 + Len001_010)
                # print('/020:',I001_020)
                Len001_020 = len(I001_020[0])
                I001_020_bin = I001_020[1]

                I001_020_bin_list = wrap(I001_020_bin, 8)
                Decoded_len = x + 2 + LenFspecHex_Cat001 + Len001_010
                for Octet_no in range(len(I001_020_bin_list)):
                    if Octet_no == 0:

                        if I001_020_bin_list[0][0] == "1":
                            Track_Info = Category001_track(data001, fspec_001_hex, fspec_001_bin, Len001_010,
                                                           Len001_020, x)
                            Track_Dic = Track_Info[0]
                            Decoded_len = Track_Info[1]
                            InfoDecodeInCat001.update({'TYP': 'Track', 'Track': Track_Dic})
                        else:
                            InfoDecodeInCat001.update({'TYP': 'Plot'})

                        if I001_020_bin_list[0][1] == "1":
                            InfoDecodeInCat001.update({'SIM': 'Simulated'})
                        else:
                            InfoDecodeInCat001.update({'SIM': 'Actual'})

                        if I001_020_bin_list[0][2:4] == "00":
                            InfoDecodeInCat001.update({'SSR/PSR': 'NoDetection'})
                        elif I001_020_bin_list[0][2:4] == "01":
                            InfoDecodeInCat001.update({'SSR/PSR': 'PSR'})
                        elif I001_020_bin_list[0][2:4] == "10":
                            InfoDecodeInCat001.update({'SSR/PSR': 'SSR'})
                        elif I001_020_bin_list[0][2:4] == "11":
                            InfoDecodeInCat001.update({'SSR/PSR': 'Combined'})

                    else:
                        if I001_020_bin_list[Octet_no][0] == "1":
                            InfoDecodeInCat001.update({'TST': 'TEST'})
                        else:
                            InfoDecodeInCat001.update({'TST': 'DEFAULT'})

                        if I001_020_bin_list[Octet_no][1:3] == "01":
                            InfoDecodeInCat001.update({'DS1_DS2': '7500'})
                        elif I001_020_bin_list[Octet_no][1:3] == '10':
                            InfoDecodeInCat001.update({'DS1_DS2': '7600'})
                        elif I001_020_bin_list[Octet_no][1:3] == '11':
                            InfoDecodeInCat001.update({'DS1_DS2': '7700'})

    return (InfoDecodeInCat001, Decoded_len)

class Asterix():
    def __init__(self, data):
        self.data = data

    def Category_048(self):
        # data_str = data
        '''InfoDecodeInCat048 = {} #为适应存储数据库格式要求，改为列表格式'''
        InfoDecodeInCat048 = []  #为适应存储数据库格式要求，改为列表格式
        # print('收到字符长度：',len(self.data))
        if self.JudgeDataLength() == True:
            Decoder048_first = Category048_track(self.data, 6)

            # print(Decoder_first)
            '''InfoDecodeInCat048.update({0: Decoder048_first[0]})   #为适应存储数据库格式要求，改为列表格式'''
            InfoDecodeInCat048.append(Decoder048_first[0])
            # print(InfoDecodeInCat062)
            DecodedDataLen_048 = Decoder048_first[1]

            # print('以解析字符长度：', DecodedDataLen)
            '''decoder048_number = 1     #为适应存储数据库格式要求，改为列表格式'''
            # print(self.data[DecodedDataLen:])
            while len(self.data) - DecodedDataLen_048 > 0:
                Decoder048_next = Category048_track(self.data[DecodedDataLen_048:], 0)
                '''InfoDecodeInCat048.update({decoder048_number: Decoder048_next[0]})   #为适应存储数据库格式要求，改为列表格式'''
                InfoDecodeInCat048.append(Decoder048_next[0])
                '''decoder048_number += 1 #为适应存储数据库格式要求，改为列表格式'''
                DecodedDataLen_048 += Decoder048_next[1]
        else:
            InfoDecodeInCat048.append({'Error': 'DataLengthError'})
        return InfoDecodeInCat048

    def Category_001(self):
        # data_str = data
        InfoDecodeInCat001 = {}
        # print('收到字符长度：',len(self.data))
        if self.JudgeDataLength() == True:
            Decoder001_first = Category001_pri_track(self.data, 6)
            # print('第一段：',Decoder001_first)
            # print('以解析第一段长度：',Decoder001_first[1])
            InfoDecodeInCat001.update({0: Decoder001_first[0]})

            # print(InfoDecodeInCat062)
            DecodedDataLen_001 = Decoder001_first[1]
            # print('返回已解析字符串长度',DecodedDataLen_001)
            # print('以解析第一段字符：',self.data[0:DecodedDataLen_001])
            # print('以解析字符长度：', DecodedDataLen)
            decoder_number_001 = 1
            # print(self.data[DecodedDataLen:])
            while len(self.data) - DecodedDataLen_001 > 0:
                Decoder001_next = Category001_pri_track(self.data[DecodedDataLen_001:], 0)
                InfoDecodeInCat001.update({decoder_number_001: Decoder001_next[0]})
                decoder_number_001 += 1
                DecodedDataLen_001 += Decoder001_next[1]
        else:
            InfoDecodeInCat001.update({'Error': 'DataLengthError'})
        return InfoDecodeInCat001

    def Category_062(self):

        # data_str = data
        '''InfoDecodeInCat062 = {}   #为适应存储数据库格式要求，改为列表格式'''
        InfoDecodeInCat062=[]
        # print('收到字符长度：',len(self.data))
        if self.JudgeDataLength() == True:
            Decoder_first = Category062_track(self.data, 6)
            '''InfoDecodeInCat062.update({0: Decoder_first[0]})     #为适应存储数据库格式要求，改为列表格式'''
            InfoDecodeInCat062.append(Decoder_first[0])    #为适应存储数据库格式要求，改为列表格式
            DecodedDataLen = Decoder_first[1]
            '''decoder_number = 1    #为适应存储数据库格式要求，改为列表格式'''
            while len(self.data) - DecodedDataLen > 0:
                Decoder_next = Category062_track(self.data[DecodedDataLen:], 0)
                '''InfoDecodeInCat062.update({decoder_number: Decoder_next[0]})   #为适应存储数据库格式要求，改为列表格式'''
                InfoDecodeInCat062.append(Decoder_next[0])
                '''decoder_number += 1    #为适应存储数据库格式要求，改为列表格式'''
                DecodedDataLen += Decoder_next[1]
        else:
            InfoDecodeInCat062.append({'Error': 'DataLengthError'})
        return InfoDecodeInCat062

    def JudgeDataLength(self):
        # print(len(self.data))
        if len(self.data) == hex2int(self.data[2:6]) * 2:
            return True
        else:
            return False











