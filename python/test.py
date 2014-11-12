from evernote.api.client import EvernoteClient
from evernote.edam.notestore.ttypes import NoteFilter, NotesMetadataResultSpec
from evernote.edam.type.ttypes import NoteSortOrder

dev_token = "S=s1:U=8fb0e:E=150ba27eb99:C=1496276bc48:P=1cd:A=en-devtoken:V=2:H=cb6610893ef12aa4b914d15d19befa09"
client = EvernoteClient(token=dev_token, sandbox=True)

NoteStore = client.get_note_store()


filterNote = NoteFilter(order=NoteSortOrder.UPDATED)
filterNote.notebookGuid = 'cdbc8617-c551-4148-b659-7ccb5d47859e'

searchResults = NoteStore.findNotes(filterNote, 0, 10)

for note in searchResults.notes:
  print note.resources
  resources = note.resources
  #try:
  print resources.getGuid
  #except:
  #  print 'whoops'

resource = NoteStore.getResource('f2db5345-2552-451e-8e21-13f9fa243450', True, False, True, False)
