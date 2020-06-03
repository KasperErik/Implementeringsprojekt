import matplotlib.pyplot as plt
plt.rcParams.update({'font.size': 22})

file = open("Opgave7.2m16.txt", "r")

lines = file.readlines()

xs = []
ys = []
s = 122072000
i = 0
for line in lines:
    elements = line.split(', ')
    xs.append(int(elements[0]))
    ys.append(int(elements[1][:-1]))

sxs = [0, 10]
sys = [s, s]

plt.scatter(xs, ys, label="Coun-Sketch medians")
plt.plot(sxs, sys, label="S", c='red')
plt.title('Cout-Sketch compared to real value with m = 16')
plt.xlabel('medians arranged ascendinly')
plt.ylabel('sum')
plt.legend()
plt.show()
