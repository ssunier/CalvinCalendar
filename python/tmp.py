#!/usr/bin/python
import sys, os, inspect, time


def main(argv):
    print 'starting the python process'
    i = 0
    #Start a simple loop that I can read from the c# commandline
    while(True):
        if (i%5==0):
            print i
        else:
            print 'this is an image'
        i=i+1
        time.sleep(20)

if __name__ == '__main__':
    main(sys.argv)
