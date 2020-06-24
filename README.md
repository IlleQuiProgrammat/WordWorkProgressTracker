# Work Progress Tracking for Word

This is a simple tool that stores the content of a word document as it changes over time.

Objectives:
 - Allow teachers to more accurately monitor a students ability rather than a student saying
   that they didn't struggle when in fact they did. This should permit appropriate help to be
   issued more quickly.
 - Discourages plagiarism by detecting changes in text over time

Save Features:
 - 5 minute intervals and at document close / save
 - Accurate time stamps
 - Accompanying program to quick fast-forward through the student's work

Missing:
 - Blockchain! (no, seriously...) The idea is to submit hashes to a database so that the teacher
   can be certain that the student has not modified the .docx file (which is trivial to do currently)
   but that is only because this program is not intended to be a _plagiarism-detecting spyware_ utility
 - Signed certificates and login (assign changes to certain users).. so yeah it can't be used in an official setting
   _yet_
 - Small documents - this currently does not save in a diff format and makes slightly larger word documents
 
 I can probably add the webserver, login and diff system if there is any want.. please indicate with a sponsor :wink:
