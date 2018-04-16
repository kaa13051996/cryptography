import random
import math
import sys
import os.path
from os import listdir

def control_lenght_key(lenght_key):
    while not lenght_key.isdigit() or int(lenght_key) < 1:
        lenght_key = input("Ключ должен быть целым положительным числом: ")
    return lenght_key

def control_way(way):  
    check_Existence = os.path.exists(way)
    check_Access = False
    while check_Access == False:
        while check_Existence == False:
            way = input("Такого пути не существует! Введите новый: ")
            check_Existence = os.access(way, os.F_OK)       
        if way[-1] != '\\':
           way += '\\'
        try:
           f = open(way + 'test', 'w')
           f.close()
        except PermissionError:
           way = input("Не хватает прав доступа! Введите другой путь: ")
           check_Access = False
           check_Existence = False
        else:
            check_Access = True
            print("Введенный вами путь корректен!")
    os.remove(way + 'test')
    return way

def gcd(a,b):
    while a != b:
        if a > b:
            a = a - b
        else:
            b = b - a        
    return a

def gcdex(a, b):
    if b == 0:
        return a, 1, 0
    else:
        d, x, y = gcdex(b, a % b)
        return d, y, x - y * (a // b)

def TestFarm(n):
    PseudoSimple = {561, 1105, 1729, 2465, 2821, 6601, 8911, 10585, 15841, 29341, 41041, 46657, 52633, 62745, 63973, 75361} #псевдопростые числа кармайкла    
    a_gen = [random.randint(0, 1000) for I in range(10)]
    if set(a_gen).issubset(PseudoSimple):
        a_gen.difference_update(PseudoSimple)
    for i in range(len(a_gen)):       
        if (pow(a_gen[i],n-1,n) == 1):
            if i == 9:
                return True
            else:
               continue           
        else:
            return False
 
def fun_chr(text):
    s = list(map(lambda x: chr(x), list(text)))      
    return(s)

def ascii_code(text):    
    s = list(map(lambda x: ord(x), list(text)))      
    return(s)

def fun_gen(a):    
    #длина ключа
    lenght_key = int(control_lenght_key(input("Длина ключа: ")))   
    #нахождение p и q
    p,q = 0,0
    bool_candidate_a = False
    while (bool_candidate_a != True):
        candidate_a = random.randint(pow(10, lenght_key-1), pow(10, lenght_key))
        bool_candidate_a = TestFarm(candidate_a)
    p = candidate_a
    bool_candidate_b = False
    while (bool_candidate_b != True):
        candidate_b = random.randint(pow(10, lenght_key-1), pow(10, lenght_key))
        bool_candidate_b = TestFarm(candidate_b)
    q = candidate_b    
    #нахождение n, f, e, d
    n = p*q
    f = (p-1)*(q-1)   
    bool_e = 0
    while (bool_e != 1):
        e = random.randint(2, f)
        bool_e = gcd(e, f)   
    d = (gcdex(e, f)[1])%f    
    #запись открытого ключа
    way = control_way(input("Путь (вида D:\, к примеру), в котором будет созданы 2 файла: "))
    f = open(os.path.join(way, "open_key.pub"), 'w')
    f.write(str(e) + "\n" + str(n))
    f.close()
    #запись закрытого ключа
    f = open(way +'priv_key', 'w')    
    f.write(str(d) + "\n" + str(n))
    f.close()
    print("Пара ключей создана!")
    chouse(input("\n---------------------\nВаш выбор: "))

def fun_enc(a):     
    way = control_way(input("Путь (вида D:\, к примеру) к папке, где содержится файл open_key.pub: "))
    #проверка на наличие нужного файла
    check_File_Presence = os.access(way + 'open_key.pub', os.F_OK)
    while not check_File_Presence:
        way = control_way(input("В данной папке отсутствует нужный файл! Выберите другую папку: "))
        check_File_Presence = os.access(way + 'open_key.pub', os.F_OK)
    f = open(os.path.join(way, "open_key.pub"))
    #считывает построчно файл, каждая строка - элемент списка
    open_key = f.readlines()       
    e = int(open_key[0])
    n = int(open_key[1])
    print('Открытый ключ:\ne = ', e, '\nn = ', n)          
    f.close()
    #извлечение текста из файла
    way_text = input("Введите путь до файла, содержащего текст: ")    
    f = open(way_text)
    text = f.read()    
    f.close()
    #получение кодов ascii 
    ascii_text = ascii_code(text)
    #возведение в степеньпо модулю    
    ascii_text = list(map(lambda x: pow(int(x), e, n), ascii_text))
    #запись результата в строку файла
    secret_text = ' '.join(map(str, ascii_text))
    f = open(way + 'secret_text.txt', 'w') 
    f.write(secret_text)
    f.close()
    print("Внимание: шифртекст будет помощён рядом с ключами!\nЗашифрование произведено!")
    chouse(input("\n---------------------\nВаш выбор: "))

def fun_dec(a):
    #считываем ключ   
    way = control_way(input("Путь (вида D:\, к примеру) к папке, где содержится файл priv_key: "))
    check_File_Presence = os.access(way + 'priv_key', os.F_OK)
    while not check_File_Presence:
        way = control_way(input("В данной папке отсутствует нужный файл! Выберите другую папку: "))
        check_File_Presence = os.access(way + 'open_key.pub', os.F_OK)
    f = open(os.path.join(way, "priv_key")) 
    priv_key = f.readlines()       
    d = int(priv_key[0])
    n = int(priv_key[1])
    print('Закрытый ключ:\nd = ', d, '\nn = ', n)          
    f.close() 
    #считываем шифртекст
    way_text = input("Введите путь до файла, содержащего шифртекст: ")    
    f = open(way_text)
    priv_text = list(f.read().split(' '))      
    f.close() 
    text = list(map(lambda x: pow(int(x), d, n), priv_text)) 
    text = fun_chr(text)
    original = "".join(text)
    f = open(way + 'original.txt', 'w') 
    f.write(original)
    f.close()    
    print("Расшифрованный текст original.txt будет помещен рядом с закрытым ключом!\nРасшифрование произведено!")
    chouse(input("\n---------------------\nВаш выбор: "))

def chouse(a):          
        if a == "1":
            print ("\n***Генерация ключей***\n")
            fun_gen(a)
        elif a == "2":
            print("\n***Зашифрование***\n")
            fun_enc(a)
        elif a == "3":
            print("\n***Расшифрование***\n")
            fun_dec(a)
        elif a == "4":
           print ("Выключение!\n")
        else:
            print("Было введено некорректное значение!")
            chouse(input("\n---------------------\nВаш выбор: "))           

chouse(input("Выберите операцию:\n1 - генерация ключей;\n2 - зашифрование;\n3 - расшифрование;\n4 - выход.\n---------------------\nВаш выбор: "))



