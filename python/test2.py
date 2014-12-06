import os

for filename in os.listdir('images'):
    print filename
    readFile = "images/" + filename
    print readFile
    with open(readFile, 'rb') as f:
        data = f.read()
    writeFile = "newImages/" + filename
    print writeFile
    with open(writeFile, 'wb') as f:
        f.write(data)
