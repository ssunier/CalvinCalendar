#!/usr/bin/python
import sys, os, inspect, time

def main(argv):
    print 'starting the python process'

    #Start a simple loop that I can read from the c# commandline
    while(True):
        print 'this is an image'
        time.sleep(10)

if __name__ == '__main__':
    main(sys.argv)
