import tkinter as tk
import ttkbootstrap as ttk

root = tk.Tk()
style = ttk.Style()

b1 = ttk.Button(root, text="Submit", bootstyle='success')
b1.pack(side=tk.LEFT, padx=5, pady=10)

b2 = ttk.Button(root, text="Submit", bootstyle='info-outline')
b2.pack(side=tk.LEFT, padx=5, pady=10)

root.mainloop()
