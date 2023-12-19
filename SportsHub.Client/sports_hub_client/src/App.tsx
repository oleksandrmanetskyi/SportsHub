import React, { FC } from "react";
import "antd/dist/antd.css";
import classes from './App.module.css';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import HeaderContainer from "./components/Header/HeaderContainer";
import PrivateLayout from "./components/PrivateLayout/PrivateLayout";
import SignUp from "./pages/SignUp/SignUp";
import SignIn from "./pages/SignIn/SignIn";
import FooterContainer from "./components/Footer/FooterContainer";
import Home from "./pages/Home/Home";
import CssBaseline from '@mui/material/CssBaseline';
import Places from "./pages/Places/Places.jsx";
import RouteWithLayout from "./components/PrivateLayout/RouteWithLayout";
import Profile from "./pages/Profile/Profile";
import TrainingProgramsPage from "./pages/TrainingProgram/TrainingPrograms";
import CreateTrainingProgram from "./pages/TrainingProgram/Create/CreateTrainingProgram";
import TrainingProgramPage from "./pages/TrainingProgram/Program/TrainingProgram";
import NutritionPage from "./pages/Nutritions/Program/Nutrition";
import NutritionsPage from "./pages/Nutritions/Nutritions";
import CreateNutritionPage from "./pages/Nutritions/Create/CreateNutritionPage";
import News from "./pages/News/News";
import CreateNewsPage from "./pages/News/Create/CreateNewsPage";
import NewsPage from "./pages/News/NewsPage/NewsPage";
import Shops from "./pages/Shops/Shops";
import CreateShopPage from "./pages/Shops/Create/CreateShopPage";
import Shop from "./pages/Shops/Shop/Shop";

const App: FC = () => (
    <div className={classes.App}>
    <CssBaseline />
    <Router>
      <HeaderContainer />
      <div className={classes.mainContent} >
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/signin" component={SignIn} />
          <Route path="/signup" component={SignUp} />
          <RouteWithLayout layout={PrivateLayout} path="/places" component={Places} />
          <RouteWithLayout layout={PrivateLayout} path="/profile" component={Profile} />
          <RouteWithLayout layout={PrivateLayout} exact path="/training-program" component={TrainingProgramsPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/training-program/new" component={CreateTrainingProgram} />
          <RouteWithLayout layout={PrivateLayout} exact path="/training-program/edit/:id" component={CreateTrainingProgram} />
          <RouteWithLayout layout={PrivateLayout} exact path="/training-program/get/:id" component={TrainingProgramPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/nutritions" component={NutritionsPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/nutritions/new" component={CreateNutritionPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/nutritions/edit/:id" component={CreateNutritionPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/nutritions/get/:id" component={NutritionPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/news" component={News} />
          <RouteWithLayout layout={PrivateLayout} exact path="/news/new" component={CreateNewsPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/news/edit/:id" component={CreateNewsPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/news/get/:id" component={NewsPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/shops" component={Shops} />
          <RouteWithLayout layout={PrivateLayout} exact path="/shop/new" component={CreateShopPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/shop/edit/:id" component={CreateShopPage} />
          <RouteWithLayout layout={PrivateLayout} exact path="/shop/get/:id" component={Shop} />
          </Switch>
          
          </div>
          <FooterContainer />
          </Router>
          
          </div>
 );
 export default App;
