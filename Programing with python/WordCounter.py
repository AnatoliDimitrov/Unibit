import re
file = open("speech.txt", "r")

myString = file.read()

myString = re.sub('[^A-Za-z0-9 ]+', '', myString)

textList = myString.split(" ")

words = {}
wordCounter = 0

for word in textList:
    wordCounter += 1
    if word not in words.keys():
        words[word] = 0
    
    words[word] += 1
    
words = dict(sorted(words.items(), key=lambda x: x[1], reverse=True))

open("result.txt", "w").close()
file = open("result.txt", "a")

file.write("Total Words - " + str(wordCounter))
file.write("\nUnique words - " + str(len(words)))
file.write("\n")

for pair in words:
    file.write("\n" + pair + " - " + str(words[pair]))

file.close()