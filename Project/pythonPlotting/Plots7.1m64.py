import matplotlib.pyplot as plt
plt.rcParams.update({'font.size': 22})

file = open("Opgave7.1m64.txt", "r")

lines = file.readlines()

xs = []
ys = []
s = 0
i = 0
for line in lines:
    if i < 100:
        elements = line.split(', ')
        xs.append(int(elements[0]))
        ys.append(int(elements[1][:-1]))
    else:
        elements = line.split(' = ')
        if elements[0] == 'S':
            s = int(elements[1][:-1])
    i += 1

sxs = [0, 100]
sys = [s, s]

plt.scatter(xs, ys, label="Coun-Sketch results")
plt.plot(sxs, sys, label="S", c='red')
plt.title('Cout-Sketch compared to real value wiht m = 64')
plt.xlabel('test results arranged ascendinly')
plt.ylabel('Actual sum')
plt.legend()
plt.show()
