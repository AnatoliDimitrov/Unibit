str1 = 'abcdef'
aar = [*str1]
print(type(aar))  # towa e list ne array - за array трябва import numpy
print(aar)
print(aar[1])
aar[1]='dddd'
print(str(aar))
print("".join(aar))