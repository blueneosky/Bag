#!/bin/bash

source env.sh
source web_layer.sh
source html_common.sh


html_print_login_page() {
	local query_data=$(http_get_query_data)
	local nok_status=$(http_data_get_get_value "$query_data" "nok")

    printf "<!DOCTYPE html>"
    printf "<html>"
	html_print_head_content
    printf "<body>"
#	env | sed  's#$#<br/>#'
	printf "<div class='main_div'>"
    printf "	<h1>NouNours Station</h1>"
	printf "	<form action='%s' method='post' class='login_form'>" "$SCRIPT_NAME"
	printf "	<div class='login_container_div'>"
	printf "	<table class='login_table'>"
	printf "	<tbody>"

	if [ "$nok_status" = "loginfailed" ]; then
		printf "		<tr class='login_table_tr'><td class='label_login_table_td'>"
		printf "			<i>Unkwon username or bad password</i>"
		printf "		</td></tr>"
	fi

	printf "		<tr class='login_table_tr'><td class='label_login_table_td'>"
	printf "			<label for='%s' class='login_label'><b>Username</b></label>" $LOGIN_NAME
	printf "		</td></tr>"
	printf "		<tr class='login_table_tr'><td class='login_table_td'>"
	printf "			<input type='text' placeholder='Enter Username' name='%s' class='login_input' required>" $LOGIN_NAME
	printf "		</td></tr>"
	printf ""
	printf "		<tr class='login_table_tr'><td class='label_login_table_td'>"
	printf "			<label for='%s' class='login_label'><b>Password</b></label>" $LOGIN_PASSWORD
	printf "		</td></tr>"
	printf "		<tr class='login_table_tr'><td class='login_table_td'>"
	printf "			<input type='password' placeholder='Enter Password' name='%s' class='login_input' required>" $LOGIN_PASSWORD
	printf "		</td></tr>"
	printf ""
	printf "		<tr class='login_table_tr'><td class='login_table_td'>"
	printf "			<button type='submit' class='login_button'>Login</button>"
	printf "		</td></tr>"
	printf "		<tr class='login_table_tr'><td class='login_table_td'>"
	printf "		<label><input type='checkbox' checked='checked' name='%s'> Remember me</label>"	$LOGIN_REMEMBER
	printf "	</td></tr>"
	printf "	</tbody>"
	printf "	</table>"
	printf "</div>"
	printf "</form>"
	html_print_bottom_page_content
	printf "</div>"
    printf "</body>"
    printf "</html>"
}

