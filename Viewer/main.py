from tkinter import *
from tkinter.scrolledtext import *
import zipfile
import os
import xml.etree.ElementTree as ET
import base64
data = [
]

with zipfile.ZipFile('example.docx', 'r') as zip_ref:
    zip_ref.extractall('./temp')

def imp(filename):
    root = ET.parse(filename).getroot()
    if 'timestamp' not in root.attrib or root.tag != 'content':
        return
    else:
        data.append((root.attrib['timestamp'], root.text))

for file in os.listdir('./temp/customXml'):
    if os.path.isfile(os.path.join('./temp/customXml', file)):
        imp(os.path.join('./temp/customXml', file))


root = Tk()
root.title("Word Work Progress Tracker")
root.resizable(False, False)
tb = ScrolledText(root)
tb.insert(END, "Hello, slide the slider to view content")
lab = Label(root, text="This will show the time of the edited content. Progress is saved every time it is closed, opened and saved.")

def processSliderChange(position):
    tb.delete("1.0", END)
    tb.insert(END, base64.b64decode(data[int(position)][1]).replace(b'\r', b'\n'))
    lab['text'] = data[int(position)][0] + " new words: " + str(len(base64.b64decode(data[int(position)][1]).replace(b'\r', b'\n').split()) - len(base64.b64decode(data[max(0, int(position)-1)][1]).replace(b'\r', b'\n').split()))

slider = Scale(root, from_=0, to=len(data)-1, tickinterval=1, length=800, orient=HORIZONTAL, command=processSliderChange)
slider.pack()
lab.pack()
tb.pack()
root.mainloop()
