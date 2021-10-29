#!/bin/bash

#vim tokens: data_get_value data_update_value


# $1 : file path with concurrent access
# $2+: command to run with args
_flock_read() { flock -s "$1.lock" -c ${@:2}; }

# $1 : file path with concurrent access
# $2+: command to run with args
_flock_write() { flock -x "$1.lock" -c ${@:2}; }


# $1 : data file path
# $2 : data key name
_data_get_value() { sed -rn "s/^$2=([^\\n]+)$/\\1/p" "$1"; }
data_get_value() { _flock_read "$1" _data_get_value $@; }

# $1 : data file path
# $2 : data key name
# $3 : value
_data_update_value() {
	if ! grep -R "^[#]*\s*$2=.*" "$1" > /dev/null; then
		echo "$2=$2" >> $1
	else
		sed -ir "s/^[#]*\s*$2=.*/$2=$3/" "$1"
	fi
}
data_update_value() { _flock_write "$1" _data_update_value $@; }

