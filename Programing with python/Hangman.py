word = 'apple'
lives = 10

wordList = [*word]
guessList = []

for i in range(len(wordList)):
    guessList.append("_");

def printInfo(guessList, lives):
    """Printing Information Function"""
    print(guessList)
    print("Lives left: {}".format(lives))

printInfo(guessList, lives)

while "_" in guessList and lives > 0:
    char = input("Guess character: ")
    noChar = True
    if char.lower() in guessList:
        lives -= 1
        printInfo(guessList, lives)
        continue

    for i in range(len(wordList)):
        if wordList[i].lower() == char.lower():
            guessList[i] = wordList[i]
            noChar = False
            
    
    if noChar:
        lives -= 1
    
    printInfo(guessList, lives)

if "_" in guessList:
    print("No more lives!")
else:
    print("The word is {}. Congrats!!!".format(word))