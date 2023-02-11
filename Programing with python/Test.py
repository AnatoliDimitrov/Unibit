def square(number):
    if isinstance(number, int) or isinstance(number, float):
        return number * number
    else:
        return None


print(square(5))

file = open("test.txt", "w")
file.write("Do nulla mollit exercitation aliquip sunt consectetur deserunt do.")

file = open("test.txt", "r")
print(file.read())