#!/bin/bash

source env.sh
source log_layer.sh
source data_layer.sh


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

# $1 : name
# $2 : pass
user_is_valid() {
	export LC_ALL=C
	expect << EOF
		log_user 0
		spawn su $1 -c "exit" 
		expect "Password:"
		send "$2\r"
		set wait_result  [wait]
		if { [lindex \$wait_result 2] == 0 } then {
			exit [lindex \$wait_result 3]
		} else {
			exit 1
		}
EOF
}

