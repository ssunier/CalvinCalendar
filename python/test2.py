import os

print "Inside the python script and about to execute the copying"

for filename in os.listdir('C:/Users/Orit/Documents/GitHub/CalvinCalendar/python/images/'):
    #print filename
    readFile = 'C:/Users/Orit/Documents/GitHub/CalvinCalendar/python/images/' + filename
    #print readFile
    with open(readFile, 'rb') as f:
        data = f.read()
    writeFile = 'C:/Users/Orit/Documents/GitHub/CalvinCalendar/Calvin/Resources/' + filename
    print "Wrote the file: ", writeFile
    with open(writeFile, 'wb') as f:
        f.write(data)
