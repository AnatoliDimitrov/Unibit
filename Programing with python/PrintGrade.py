def printGrade(name='no name', score=0, *params) -> str:
    if score < 0 and score > 100:
        result = '{} is NOT between 0 and 100'
    elif 0 <= score < 60:
        result = (sentence + "F").format(name)
    elif 60 <= score < 70:
        result = (sentence + 'D').format(name)
    elif 70 <= score < 80:
        result = (sentence + 'C').format(name)
    elif 80 <= score < 90:
        result = (sentence + 'B').format(name)
    elif 90 <= score < 100:
        result = (sentence + 'A').format(name)
        
    return result


pointsPossible = 100
wrongInput = True
name = input('Please input name: ')
while wrongInput:
    try:
        score = int(input("Input percentage: "))
        wrongInput = False
    except Exception as e:
       # print("The input is not a number! Please input an integer.")
        print(e)
        wrongInput = True
sentence = "{}'s grade is: "

print(printGrade(name, score))