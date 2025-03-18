import cv2
import numpy as np


class proc():
    def __init__(self,img,size,):
        self.img = img
        self.size = size
        self.cord = (0,0,0)


    def find(self):
        min = np.array([130, 0, 64])
        max = np.array([255, 255, 200])
        #origin = (int(self.size[0]/2),int(self.size[1]/2))
        while True:
            if True:
                img = cv2.imread(self.img)
                try:
                    hsv = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
                    mask = cv2.inRange(hsv, min, max)
                    contours, _ = cv2.findContours(mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
                    if (len(contours) > 0):
                        x, y, w, h = cv2.boundingRect(contours[0])
                        center = (int((x + x + w) / 2), int((y + y + h) / 2))
                        cv2.rectangle(img, (x, y), (x + w, y + h), (0, 0, 255), 2)
                        img = cv2.circle(img, center, 3, (0, 0, 255), 3)
                        self.cord  = (center[0],center[1],1)
                        #print(self.cord)
                    else:
                        self.cord=(0,0,0)
                    cv2.imshow("Normal", img)
                    cv2.waitKey(100)
                except:
                    pass
