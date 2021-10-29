#!/bin/bash

send_magic_packet() {
	wakeonlan $CLIENT_MAC 2>&1 > /dev/null 
}

send_longsleep_packet () {
#	echo "fait dodo" | netcat ?$TARGET_IP? $CLIENT_PORT &
	local BROADCAST_IP=$(ip -4  address | grep -P "inet.*?brd" | sed -e "s/^.*brd //" -e "s/ /\n/" | awk "NR==1 {print; exit}")
	echo "fait dodo" | socat - "UDP-DATAGRAM:$BROADCAST_IP:$CLIENT_PORT,broadcast"
}

