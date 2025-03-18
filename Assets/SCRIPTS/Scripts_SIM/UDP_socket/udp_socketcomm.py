import socket
import time

udp_client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

server_address = ('localhost', 12345)

try:
    while True:
        #command = input("Enter command (forward, backward, right or left): ")
        for i in range (1,1000):
            command = "forward"
            udp_client.sendto(command.encode(), server_address)
            time.sleep(0.01)
        for i in range (1,1000):
            command = "right"
            udp_client.sendto(command.encode(), server_address)
            time.sleep(0.01)
        for i in range (1,1000):
            command = "rotateR"
            udp_client.sendto(command.encode(), server_address)
            time.sleep(0.01)
        for i in range (1,1000):
            command = "rotateL"
            udp_client.sendto(command.encode(), server_address)
            time.sleep(0.01)
      
finally:
    udp_client.close()