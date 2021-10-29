#!/bin/bash

source data_layer.sh

if [ "$REQUEST_METHOD" = "POST" ]; then
	process_login
	exit 0
fi

local COOKIE_SESSION_ID=$(get_cookie_value "$COOKIE_SESSION_ID_NAME")
if [ -z "$COOKIE_SESSION_ID" ]; then
	print_header
	show_login_page
	exit 0
fi

print_header


if [ "$QUERY_STRING" = "$ACTION_WAKEUP" ]; then
	send_magic_packet
	show_delayed_redirect "$SCRIPT_NAME" 3
	exit 0
fi

if [ "$QUERY_STRING" = "$ACTION_LONGSLEEP" ]; then
	send_longsleep_packet
	show_delayed_redirect "$SCRIPT_NAME" 3
	exit 0
fi

show_main_page
exit 0

