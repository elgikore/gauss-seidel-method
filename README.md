I made this one-off project as a calculator for my CPE2237 Numerical Methods subject.

# What is the Gauss-Seidel Method
It is a method used in solving systems of equations iteratively, rather than using the usual Gauss-Jordan elimination method. Since it is iterative, it is fast 
even to the largest matrices, at the cost of precision because the $x$ values are an approximation.

Gauss-Seidel and Gauss-Jacobi are very similar in execution, but the feature that sets them both apart is that the former's $x$ values are modified as soon as the previous
value is evaluated (i.e. after evaluating $x_1$, the value is used as part of calculating $x_2$ and so on, leading to faster convergence. What's common for them however,
is that you need a (strictly) diagonally dominant matrix in order for it to work — or else you'll be stuck in an infinite loop. 

For a 3x3 augmented matrix, where the coefficients ($a$'s) of the 3x3 matrix are on the left side and the resultant vector $B$ (represented by $b$'s) on the right side,<br><br>
<img src="https://latex.codecogs.com/png.image?\dpi{110}\bg{white}\begin{bmatrix}&a_{11}&a_{21}&a_{31}&|&b_1\\&a_{21}&a_{22}&a_{32}&|&b_2\\&a_{31}&a_{23}&a_{33}&|&b_3\\\end{bmatrix}" title="\begin{bmatrix}&a_{11}&a_{21}&a_{31}&|&b_1\\&a_{21}&a_{22}&a_{32}&|&b_2\\&a_{31}&a_{23}&a_{33}&|&b_3\\\end{bmatrix}" height=100/>
<br><br>the formula is:<br><br>
![lagrida_latex_editor](https://github.com/user-attachments/assets/1dff7121-d6ba-4562-8ed1-2062048f2f72)

Where:
* $a_{ij}$ = Coefficients ($a_{11}, a_{12},$ etc.)
* $b_i$ = Resultant vector's ($B$) coefficients ($b_1, b_2,$ etc.)
* $k$ - Iteration №

A 3x3 matrix is said to be diagonally dominant if it satisfies all three condiitons:<br><br>
![lagrida_latex_editor (1)](https://github.com/user-attachments/assets/4aae35eb-9312-4168-bfc0-afa846e098dd)


