import socket
import time

class CL():
    def __init__(self,localhost,portSend,port):
        self.client = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
        self.localHost = localhost
        self.portSend=portSend
        self.port = port
        self.client.bind((self.localHost, self.port))
        self.massage=0
        self.senchron=0
        self.Start=False

    def send(self):
        while True:
            time.sleep(1)
            self.client.sendto(f"{self.massage}".encode(), (self.localHost, self.portSend))

    def sendSim(self):
        self.client.sendto(f"{self.massage}".encode(), (self.localHost, self.portSend))