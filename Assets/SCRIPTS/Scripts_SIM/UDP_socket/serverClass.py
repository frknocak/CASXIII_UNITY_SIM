import socket
import queue
import time

class SV():

    def __init__(self,localhost,port):
        self.massages = queue.Queue()
        self.clients = []
        self.localHost = localhost
        self.port = port
        self.server = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
        self.server.bind((self.localHost,self.port))
        self.msg= "-------"

    def recieve(self):
        while True:
            try:
                massage,addr = self.server.recvfrom(1024)
                self.massages.put((massage,addr))
            except:
                pass

    def broadcast(self):
        while True:
            while not self.massages.empty():
                massage, addr= self.massages.get()
                self.msg=massage.decode()
            time.sleep(0.25)