import clientClass
import serverClass
import threading
import multiprocessing
import time
import imgProcc


class auto():
    def __init__(self):
        self.client = clientClass.CL("127.0.0.1",12345,1234)

        self.img = imgProcc.proc(r"Screenshots\img.png",(640,360))

        self.th1 = threading.Thread(target=self.client.send)
        self.th2 = threading.Thread(target=self.img.find)

        self.th2.start()


    def random(self):
        pathList=[r"Screenshots\img.png",r"Under Screenshots\img.png"]
        scanList=["rotateR", "rotateL","rotateL","rotateR","forward"]
        counter=0
        beSure=0
        scan=True
        found=False
        scanCnt =0
        turnAngle=200
        self.client.massage = "forward"
        for i in range(0, 20):
            self.client.sendSim()
            time.sleep(0.1)
        while True:
            if scan:
                if(False):
                    pass
                else:
                    cnt = 0
                    self.client.massage = scanList[cnt]
                    if (scanCnt == 3 ) :
                        while cnt < turnAngle:
                            self.client.sendSim()
                            if self.img.cord[2] == 1:
                                if (beSure > 10):
                                    scan = False
                                    foundCord = self.img.cord
                                    break
                                elif (beSure == 3):
                                    cnt = 4
                                    turnAngle = int(turnAngle / 2)
                                    beSure = beSure + 1
                                else:
                                    beSure = beSure + 1
                            else:
                                beSure = 0
                            time.sleep(0.05)
                            cnt = cnt + 1
                    elif(scanCnt == 0):
                        while cnt < turnAngle:
                            self.client.sendSim()
                            if self.img.cord[2] == 1:
                                if (beSure > 10):
                                    scan = False
                                    foundCord = self.img.cord
                                    break
                                elif (beSure == 3):
                                    cnt = 4
                                    turnAngle = int(turnAngle / 2)
                                    beSure = beSure + 1
                                else:
                                    beSure = beSure + 1
                            else:
                                beSure = 0
                            time.sleep(0.05)
                            cnt = cnt + 1
                    elif(scanCnt == 1 or scanCnt == 2):
                        while cnt < turnAngle:
                            self.client.sendSim()
                            if self.img.cord[2] == 1:
                                if (beSure > 10):
                                    scan = False
                                    foundCord = self.img.cord
                                    break
                                elif (beSure == 3):
                                    cnt = 4
                                    turnAngle = int(turnAngle / 2)
                                    beSure = beSure + 1
                                else:
                                    beSure = beSure + 1
                            else:
                                beSure = 0
                            time.sleep(0.05)
                            cnt = cnt + 1
                    else:
                        while cnt < turnAngle:
                            self.client.sendSim()
                            if self.img.cord[2] == 1:
                                if (beSure > 10):
                                    scan = False
                                    foundCord = self.img.cord
                                    break
                                elif(beSure == 3):
                                    cnt = 4
                                    turnAngle = int(turnAngle / 2)
                                    beSure = beSure + 1
                                else:
                                    beSure = beSure + 1
                            else:
                                beSure = 0
                            time.sleep(0.05)
                            cnt = cnt + 1
                    scanCnt= scanCnt + 1
                    scanCnt=scanCnt%5
                    print(scanCnt, cnt, scan, self.client.massage)
            else:
                while (self.img.cord[2] == 1):
                    diff= self.img.cord[0] - 320
                    getClose = int((360 - self.img.cord[1])/20)
                    if diff > 5:
                        for i in range(0, diff):
                            self.client.massage = "rotateR"
                            print("Detected",self.client.massage)
                            self.client.sendSim()
                            time.sleep(0.05)
                    elif diff < -5:
                        diff = -diff
                        for i in range(0,diff):
                            self.client.massage = "rotateL"
                            print("Detected", self.client.massage)
                            self.client.sendSim()
                            time.sleep(0.05)
                    else:
                        if getClose > 0:
                            for i in range(0,getClose):
                                self.client.massage = "forward"
                                print("Detected", self.client.massage)
                                self.client.sendSim()
                                time.sleep(0.05)
                        else:
                            self.client.massage = "forward"
                            print("Detected", self.client.massage)
                            self.client.sendSim()
                            time.sleep(0.05)
                counter = (counter + 1)%2
                self.img.img = pathList[counter]
                cnt = 0
                while self.img.cord[2]== 0 and cnt < 50:
                    self.client.massage = "forward"
                    self.client.sendSim()
                    time.sleep(0.05)
                    cnt = cnt + 1
                    print(cnt)
                if cnt == 50:
                    scan =True
                    counter = (counter + 1) % 2
                else:
                    while True:
                        xDiff = (self.img.cord[0] - 320)
                        yDiff = (self.img.cord[1] - 180)
                        if(abs(xDiff)<10 and abs(yDiff)<10):
                            self.client.massage = "down"
                            self.client.sendSim()
                            time.sleep(0.05)
                        else:
                            if (xDiff > 0):
                                self.client.massage = "right"
                                self.client.sendSim()
                                time.sleep(0.05)
                            else:
                                self.client.massage = "left"
                                self.client.sendSim()
                                time.sleep(0.05)
                            if (yDiff > 0):
                                self.client.massage = "backward"
                                self.client.sendSim()
                                time.sleep(0.05)
                            else:
                                self.client.massage = "forward"
                                self.client.sendSim()
                                time.sleep(0.05)



if __name__ == '__main__':
    a=auto()
    a.random()