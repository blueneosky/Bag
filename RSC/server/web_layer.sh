#!/bin/bash

source env.sh


# $1 : cookie name
http_cookie_get_value() {
	echo "$HTTP_COOKIE" | sed 's/; /\n/g' | grep "$1=" | sed 's/^.*=//'
}

http_print_header() {
	printf "Content-type: text/html\n"
	local COOKIE_SESSION_EXPIRATION_DATE=$(date -d "+1 years" +'%a, %d %b %Y %T GMT')	#for 1 year, we don't care to adjust time zone ;-)
#	printf "Set-Cookie: dummy1=lol\n"
#	printf "Set-Cookie: %s=none; HttpOnly; Secure; SameSite=Strict; Expires=%s'\n" "$COOKIE_SESSION_ID_NAME" "$COOKIE_SESSION_EXPIRATION_DATE"
#	printf "Set-Cookie: dummy2=lol\n"
	printf "\n"
}

http_get_query_data() {
	# extract data from query string
	local hgpd_data=$(echo "$QUERY_STRING" | sed -e 's/%/\\x/g' -e 's/&/\n/g')
	echo -e "$hgpd_data"
}

http_get_post_data() {
	# extract data from post content
	local hgpd_data=$(sed -e 's/%/\\x/g' -e 's/&/\n/g' <&0)
	echo -e "$hgpd_data"
}

# $1 : data from http_get_request_data or http_get_post_data
# $2 : key
http_data_get_get_value() {
	 echo "$1" | grep "^$2=" | cut '-d=' -f2
}

