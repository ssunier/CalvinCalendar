import os

img_list = []

path = 'images/'
listing = os.listdir(path)
for infile in listing:
    print "current file is: " + infile
    img_list.append(infile)

#for filename in os.listdir('images'):
for filename in img_list:
    print "in for loop"
    print filename
    with open(filename, 'rb') as f:
        data = f.read()
    print "made it here"
    with open(filename, 'wb') as f:
        f.write(data)
    print "supposedly wrote file"
