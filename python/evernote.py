#!/usr/bin/python
import sys, os

# import evernote packages
from evernote.api.client import EvernoteClient
from evernote.edam.notestore.ttypes import NoteFilter, NotesMetadataResultSpec
from evernote.edam.type.ttypes import NoteSortOrder


'''
save_files method
makes a call to the evernote client to get the image file
saves that file in a folder
'''
def save_files(NoteStore, guid):
  resource = NoteStore.getResource(guid, True, False, True, False)
  # get the file content so you can save it
  file_content = resource.data.body
  file_name = resource.attributes.fileName

  #save the file into the output folder
  file_save = open('notes/' + file_name, 'w')
  file_save.write(file_content)
  file_save.close()

  return guid

  

'''
main method
runs in the background once the program has been started
crawling evernote for new notes in the livescribe notebook
'''
def main(argv):
  # the list of already entered images
  guidList = []
  # Evernote login credentials
  dev_token = "S=s1:U=8fb0e:E=150ba27eb99:C=1496276bc48:P=1cd:A=en-devtoken:V=2:H=cb6610893ef12aa4b914d15d19befa09"
  client = EvernoteClient(token=dev_token, sandbox=True)
  NoteStore = client.get_note_store()
  filterNote = NoteFilter(order=NoteSortOrder.UPDATED)
  filterNote.notebookGuid = 'cdbc8617-c551-4148-b659-7ccb5d47859e'
  searchResults = NoteStore.findNotes(filterNote, 0, 20)
  
  for note in searchResults.notes:
    if note.resources != None:
      for r in note.resources:
        # save each livescribe file and add the guid to the list
        guidList.append(save_files(NoteStore, r.guid))
  
  # now you start the evernote listening thread
  while(True):
    newNote = NoteStore.findNotes(filterNote, 0, 1)
    for note in newNote.notes:
      if note.resources != None:
        for r in note.resources:
          if not (r.guid in guidList):
            guidList.append(save_files(NoteStore, r.guid))
          else:
            print(r.guid)

if __name__ == '__main__':
    main(sys.argv)