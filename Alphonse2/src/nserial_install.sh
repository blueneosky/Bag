#!/usr/bin/env bash

#unsure the current cwd
cd "$(dirname "$0")"

#script fail at first error
set -euo pipefail

echo "[Step] root right ..."
sudo echo "acquired"

echo "[Step] git clone ..."
if [[ ! -d serialportstream ]]; then
    git clone https://github.com/jcurl/serialportstream.git
fi

echo "[Step] make ..."
cd serialportstream/dll/serialunix
[[ ! -d build ]] && mkdir build
cd build
cmake .. && make

echo "[Step] install..."
sudo make install
