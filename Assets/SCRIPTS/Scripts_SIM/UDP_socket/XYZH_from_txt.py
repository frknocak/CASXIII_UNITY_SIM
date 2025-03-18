import socket
import time

def send_to_udp_port(file_path, udp_port):
    # Create a UDP socket
    udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    try:
        # Open the file and read lines
        with open(file_path, 'r') as file:
            for line in file:
                # Send each line to the UDP port
                udp_socket.sendto(line.encode('utf-8'), ('localhost', udp_port))
                print(f"Sent: {line.strip()}")
                time.sleep(0.01)

    except FileNotFoundError:
        print(f"Error: File '{file_path}' not found.")
    except Exception as e:
        print(f"Error: {e}")

    finally:
        # Close the socket
        udp_socket.close()
        

# Replace 'your_file.txt' with the path to your text file
file_path = "C:/Users/Furkan/Desktop/imu_control_verileri.txt"
udp_port = 12345

# Call the function to send lines to the UDP port
send_to_udp_port(file_path, udp_port)
