#!/usr/bin/env python
# coding: utf-8

# In[ ]:


import os
from PyPDF2 import PdfFileWriter, PdfFileReader, PdfFileMerger

def pdfSpliter(pdfName, max_size):

    inputpdf = PdfFileReader(open(pdfName, "rb"))
    if not os.path.exists("output/" + pdfName[:-4]):
        os.makedirs("output/" + pdfName[:-4])
    
    
    output = PdfFileWriter()
    output.addPage(inputpdf.getPage(0))
    with open("output/" + pdfName[:-4] +"/"+ str(1) +".pdf", "wb") as outputStream:
        output.write(outputStream)
    
    mega_total = 0
    total_page_size = 0
    count = 1
    
    for i in range(1,inputpdf.numPages):
        output = PdfFileWriter()
        output.addPage(inputpdf.getPage(i))
        current_pdf_name = "output/" + pdfName[:-4] +"/"+ str(count) +".pdf"
        
        with open("temp.pdf", "wb") as outputStream:
             output.write(outputStream)
        current_page_size = os.path.getsize("temp.pdf")/1024
        
        if current_page_size + total_page_size <= max_size:
            #TODO:
            merger = PdfFileMerger()
            merger.append(current_pdf_name)
            merger.append("temp.pdf")
            merger.write(current_pdf_name[:-4] + "new.pdf")
            merger.close()
            
            total_page_size += current_page_size
            
            os.remove(current_pdf_name)
            os.rename(current_pdf_name[:-4] + "new.pdf",current_pdf_name)
        else:
            count += 1
            with open("output/" + pdfName[:-4] +"/"+  str(count) +".pdf", "wb") as outputStream:
                output.write(outputStream)
            total_page_size = current_page_size
        mega_total += current_page_size
            
        print(total_page_size, count)
        # print(i)
    print(mega_total)
            
            
            
        
a=input()
b=int(input())
pdfSpliter(a, b)

