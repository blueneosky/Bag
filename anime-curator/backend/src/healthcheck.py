#!/usr/bin/env python3
import sys
import requests

def check_health(url: str | bytes) -> int:
    try:
        response = requests.get(url)
        if response.status_code == 200:
            print(f"OK - {url} is healthy")
            return 0
        else:
            print(f"FAIL - {url} returned {response.status_code}")
            return 1
    except Exception as e:
        print(f"ERROR - Could not reach {url}: {e}")
        return 1

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: healthcheck.py <url>")
        sys.exit(1)

    url = sys.argv[1]
    sys.exit(check_health(url))
