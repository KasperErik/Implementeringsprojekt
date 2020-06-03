import matplotlib.pyplot as plt
plt.rcParams.update({'font.size': 22})

file = open("Opgave3Shift.txt", "r")

lines = file.readlines()

shiftls = []
shifttimes = []

for line in lines:
    elements = line.split(', ')
    shiftls.append(int(elements[0]))
    shifttimes.append(int(elements[1][:-1]) / 1000)

file1 = open("Opgave3ModP.txt", "r")

lines = file1.readlines()

modpls = []
modptimes = []

for line in lines:
    elements = line.split(', ')
    modpls.append(int(elements[0]))
    modptimes.append(int(elements[1][:-1]) / 1000)

plt.plot(modpls, modptimes, label="multiply-mod-prime")
plt.plot(shiftls, shifttimes, label="multiply-shift")
plt.xlabel('l some bruges til at bestemme nøgle rummet')
plt.ylabel('køre tid i sekunder')
plt.legend()
plt.show()
