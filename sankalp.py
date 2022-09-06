import re

x = [i+'>' for i in re.split('<*>',open("students.xml",'r').read().replace("\n",""))]

stack = [0]

schema = {
    0:0,
    'students':1,
    'student':2,
    'name':3,
    'age':3,
    'subject':3,
    'gender':3,
}

line = 0
missing = {}

for i in x[:-1]:
    j = re.findall('<[a-zA-Z]+>',i)
    j = j if len(j)!=0 else re.findall('</[a-zA-Z]+>',i)
    k = j[0][1:-1]
    poped = 0
    if '/' not in k:
        if schema[ stack[-1]] < schema[k]:
            stack.append(k)
        else:
            missing[line]  = stack.pop()
            #poped = schema[missing[line]]
            stack.append(k)
    else:
        # new code below
        while schema[k[1:]] < schema[stack[-1]]:
            line += 1
            line_temp = line - (schema[stack[-1]] - schema[k[1:]])
            print(line_temp)
            missing[line_temp] = stack.pop()
        # new code above
        poped = schema[stack.pop()]
        
    line += 1 if poped != 3 else 0

print(missing)