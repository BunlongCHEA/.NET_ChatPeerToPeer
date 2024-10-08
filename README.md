# I. Purpose of the .NET (v.4.8) WindowForm As Mini-Project
This is mini project in .NET C# that user to message and chat with peer to peer

Require:
- Visual Studio IDE
- C# Window Form (.NET v.4.8)
- Allow Access Port OR Change To Any Desired Port 

## Step 1: Server Side - Click "Start Server" To Start Server App 
- To start Server App, click the button "Start Server". 
NOTE: Must open Server App & Click only 1 time

![1.png : Intro to the UI for Server App and start server](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/1.png)

## Step 2: On Client side - Click "Connect" To Connect And Fill In Server IP & Port
- Note that you can open multiple Client App
- You must input any desired Username, Server IP, and Port from Server app (Display warning if not input username)
- Click "Connect" to server

![2.png : Intro to the UI for Client App and start multiple client](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/2.png)
 
## Step 3: Display Log On Both Server & Client App
Once you connect to the Server side: 
- Log will be display 'connect'
- The UI - Online Status (Refresh Every 10 seconds) - will be display who online to this Server

![3.png : Display log and UI online on both server and client](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/3.png)

## Step 4: Client side - Start Interaction With Peer To Peer
On 1st Client App:
- Click on any peer on UI - Online Status (Refresh Every 10 seconds)
- You can start write any Message to that peer/friend

![4.png : Select peer who online and send the Message to peer](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/4.png)
  
On 2nd Client App,
- Your friend/peer will received the message with display name to show who sended
- Once received, he **Must** select that friend on UI - Online Status (Refresh Every 10 seconds)
- Now he can response the message back

![5.png : Select peer who online and response the Message back](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/5.png)

## Step 5: Offline Peer Either By Changing Username OR Close & Open A New Client App
Note that on Client side, 
- You can change your Username, and re-click connect.

  **Note:** The First username is "Hello". However, after changing Username to "Home", the Name "Hello" is remove from UI and add a new Name "Home" instead

- The UI - Online Status (Refresh Every 10 seconds) - will refresh every 10 second, while the offline peer will be remove

![6.png : Offline by changing username or close and open new Client App](https://github.com/BunlongCHEA/.NET_ChatPeerToPeer/blob/master/Img_README/6.png)

This is the end of the Mini-Project, Thank you for reading...
