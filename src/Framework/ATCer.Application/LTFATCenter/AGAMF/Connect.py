

import pymysql


class ConnetSQL:
    def __init__(self):
        self.conn = pymysql.connect(host='localhost', port=3307, user='root', password='..root', database='flow',
                                    charset='utf8', autocommit=1)  # 创建连接
        self.cursor = self.conn.cursor()  # 创建游标

    def SelectSQL(self, SelectStr):
        ResultSQL = ''
        try:
            self.conn.begin()
            self.cursor.execute(SelectStr)
            self.conn.commit()
            ResultSQL = self.cursor.fetchall()

        except Exception as error:
            ResultSQL = 'ERROR'
        finally:
            self.cursor.close()
            self.conn.close()
            return ResultSQL