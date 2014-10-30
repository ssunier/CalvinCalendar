from evernote.api.client import EvernoteClient

dev_token = "S=s1:U=8fb0e:E=150ba27eb99:C=1496276bc48:P=1cd:A=en-devtoken:V=2:H=cb6610893ef12aa4b914d15d19befa09"
client = EvernoteClient(token=dev_token)
userStore = client.get_user_store()
user = userStore.getUser()

#test to see that you have found the user
print user.username