from evernote.api.client import EvernoteClient
from evernote.edam.notestore.ttypes import NoteFilter, NotesMetadataResultSpec
from evernote.edam.type.ttypes import NoteSortOrder

dev_token = "S=s1:U=8fb0e:E=150ba27eb99:C=1496276bc48:P=1cd:A=en-devtoken:V=2:H=cb6610893ef12aa4b914d15d19befa09"
client = EvernoteClient(token=dev_token, sandbox=True)

NoteStore = client.get_note_store()


filterNote = NoteFilter(order=NoteSortOrder.UPDATED)
filterNote.notebookGuid = 'cdbc8617-c551-4148-b659-7ccb5d47859e'

searchResults = NoteStore.findNotes(filterNote, 0, 10)

def getImage(count, rerun):
  for note in searchResults.notes:
    if note.resources != None:
      for r in note.resources:
        guid = r.guid
        print guid
        try:
          resource = NoteStore.getResource(guid, True, False, True, False)
          # get the file content so you can save it
          file_content = resource.data.body
          #print file_content
          file_name = resource.attributes.fileName

          # save the file into the output folder
          file_save = open('../Calvin/output/' + file_name, "wb")
          file_save.write(file_content)
          file_save.close()
          print file_name
        except:
          rerun = False
          #keepRunning = True
          #print "re-running"
          #print count

count = 1
rerun = True
getImage(count, rerun)
print "finished first iteration"

if (rerun):
  print "Going to rerun"
  count+=1
  if (count <= 3):
    print "re-running: iteration " + str(count)
    getImage(count, rerun)

if (rerun):
  print "Going to rerun"
  count+=1
  if (count <= 3):
    print "re-running: iteration " + str(count)
    getImage(count, rerun)

print "evernote is shit"
  
