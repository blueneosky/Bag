#!/bin/bash

source env.sh
source log_layer.sh
source data_layer.sh
source web_layer.sh
#TODO this depends should disapear
source html_common.sh
source html_redirect.sh


# $1 : user
user_get_token() {
	local RAW=$(data_get_value "$TOKENS_FILE_PATH" "$1")
	local RAW_EXPIRAITON_DATE=$(echo "$raw" | cut -d '|' -f2)
	# need to test this case, date will return a value for empty string
	if [ -z $RAW_EXPIRAITON_DATE ]; then
		exit 1
	fi
	local EXPIRAITON_DATE=$(date -d "$RAW_EXPIRAITON_DATE" +%s 2&>1 > /dev/null)
	# empty when error (data corruption ?)
	if [ -z $EXPIRAITON_DATE ]; then
		exit 1
	fi
	local CURRENT_TIME=$(date +%s)
	# expiration occured
	if [ $CURRENT_TIME -g $EXPIRAITON_DATE  ]; then
		exit 1
	fi

	echo $(echo "$raw" | cut -d '|' -f1)
	exit 0
}

# $1 : user
# $2 : token
# $3 : expiration date
user_update_token() {
	data_update_value "$TOKENS_FILE_PATH" "$1" "$2|$3"
}

#TODO remove/split/dispatch this code ...
#TODO don't forget to update source *
process_login() {
	# extract data from post content
	data=$(sed -e 's/%/\\x/g' -e 's/&/\n/g' <&0)
	data=$(echo -e "$data")
	# extract expected values
	USER_NAME=$(    echo "$data" | grep "^$LOGIN_NAME="     | cut '-d=' -f2)
	USER_PASSWORD=$(echo "$data" | grep "^$LOGIN_PASSWORD=" | cut '-d=' -f2)
	USER_REMEMBER=$(echo "$data" | grep "^$LOGIN_REMEMBER=" | cut '-d=' -f2)

	#check user
	export LC_ALL=C
	expect << EOF
		log_user 0
		spawn su $USER_NAME -c "exit" 
		expect "Password:"
		send "$USER_PASSWORD\r"
		set wait_result  [wait]
		if { [lindex \$wait_result 2] == 0 } then {
			exit [lindex \$wait_result 3]
		} else {
			exit 1
		}
EOF
	if [ $? -ne 0 ]; then
		#nop - user not recognized or bad password (don't want to gove an hint)
		log_append_login_status "KO"

		local PL_SCRIPT_NAME=$(echo "$SCRIPT_NAME" | sed -e 's/\?.*$//')

		http_print_header
		html_print_redirect_page "${PL_SCRIPT_NAME}?nok=loginfailed"

		exit 0
	fi

	#good - login success
	log_append_login_status "OK"


	http_print_header
    printf "<!DOCTYPE html>"
    printf "<html>"
	html_print_head_content
    printf "<body>"
#	env | sed -e 's#$#<br/>#g'
	exit 0
}

