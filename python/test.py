from evernote.api.client import EvernoteClient
from evernote.edam.notestore.ttypes import NoteFilter, NotesMetadataResultSpec
from evernote.edam.type.ttypes import NoteSortOrder
import hashlib
import binascii


dev_token = "S=s1:U=8fb0e:E=150ba27eb99:C=1496276bc48:P=1cd:A=en-devtoken:V=2:H=cb6610893ef12aa4b914d15d19befa09"
client = EvernoteClient(token=dev_token, sandbox=True)

NoteStore = client.get_note_store()


filterNote = NoteFilter(order=NoteSortOrder.UPDATED)
filterNote.notebookGuid = 'cdbc8617-c551-4148-b659-7ccb5d47859e'

searchResults = NoteStore.findNotes(filterNote, 0, 1)

for note in searchResults.notes:
  if note.resources != None:
    for r in note.resources:
      #print r
      guid = r.guid
      #print r.data
      #print r.data.body
      #print r.data.size
      #print r.data.bodyHash
      #bodyhash = r.data.bodyHash
      #bodyhash = bodyhash.decode("utf-8")
      #print bodyhash
      
      #emlhash = (resource.data.bodyHash).toString('hex');
      #print emlhash
      #print r.recognition
      #print r.data.bodyHash
      #print r.recognition.bodyHash
      file_name = r.attributes.fileName

      #hash_hex = binascii.hexlify(r.data.bodyHash)
      #print hash_hex

      resource = NoteStore.getResource(guid, True, False, True, False)
      #print resource
      # get the file content so you can save it
      file_content = resource.data.body
      print file_content
      file_name = resource.attributes.fileName
      #print file_name
      # save the file into the output folder
      file_save = open('output/' + file_name, "w")
      #file_save.write(file_content)
      file_save.close()
      print file_name
