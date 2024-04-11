# Sports Hub - Test Cases
## User Test Cases

### Test Case 1: Successful User Registration
**Description**: Test for User Registration with valid inputs.
**Pre-Condition**: The user is on the registration page.
1. Enter a valid name in the name field.
2. Enter a valid email address in the email field.
3. Enter a valid password in the password field.
4. Enter the same password in the confirm password field.
5. Enter a valid surname in the surname field.
6. Enter a valid location in the location field.
7. Choose kind of sport in "Your kind of sport" field.
8. Click the 'Sign up' button.

**Expected Result**:
1. The user should be successfully registered.
2. A confirmation message should be displayed.
3. The user should be redirected to the "Home" page.
4. The user should be able to log in with the registered credentials.


### Test Case 2: Failed User Registration
**Description**: Test for User Registration with valid inputs.
**Pre-Condition**: The user is on the registration page.
1. Enter an invalid name in the name field.
2. Enter an invalid email address in the email field.
3. Enter an invalid password in the password field.
4. Enter a different password in the confirm password field.
5. Enter an invalid surname in the surname field.
6. Enter an invalid location in the location field.
7. Don't choose kind of sport in "Your kind of sport" field.
8. Click the 'Sign up' button.

**Expected Result**:
1. The user should not be registered.
2. An error validation messages should be displayed for each invalid field.
3. The user should not be able to log in with the entered credentials.

### Test Case 3: Viewing Sports News
**Description**: Test for viewing sports news.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'News' tab in Navigation Menu.
2. View the list of sports news.
3. Click on a news article to view it.

**Expected Result**:
1. The user should be able to view the list of sports news.
2. The user should be able to click on a news article and be redirected to it's page to view it.

### Test Case 4: Viewing Sports Places
**Description**: Test for viewing sports places.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'Places' tab in Navigation Menu.
2. View the google map with places to do specified in user profile kind of sport in the location specified in user profile.
3. View buttons "Next place" and "Change location".
4. Click on a sports place to view it details - name, location, rating.
5. Click "Next place" button to select the next place
6. Click the "Change location" button.

**Expected Result**:
1. The user should be able to view the google map with places to do specified in user profile kind of sport in the location specified in user profile.
2. By default, 1 place should be highlighted as recommended, user should be available to click "Next place" button to select the next place.
3. The user should be able to click the "Change location" button.
4. After clicking the "Change location" button the user should be redirected to "User profile" page.


### Test Case 5: Viewing Training Programs
**Description**: Test for viewing training programs.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'Training programs' tab in Navigation Menu.
2. View the list of training programs.
3. View filter with predefined values.
4. View pagination.
5. Select "By my sport kind" filter.
6. Select "Recommended" filter.
7. Click next pages in pagination.
8. Click on a training program to view it.

**Expected Result**:
1. The user should be able to view the list of all training programs by default.
2. After selecting "By my sport kind" filter - should be displayed only training programs corresponding to specified in user profile kind of sport.
3. After selecting "Recommended" filter - should be displayed only training programs recommended to user reflecting his old ratings of existing training programs, random training programs should be recommended only in case user doesn't have any performed rating actions.
4. User should be able to select pages in pagination, after selecting training programs from that page should be displayed.
5. The user should be able to click on a training program and be redirected to it's page to view it.