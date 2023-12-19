import React, { useState, useEffect } from "react";
import { Layout, Menu, Dropdown } from "antd";
import { LoginOutlined, LogoutOutlined, EditOutlined } from "@ant-design/icons";
import { NavLink } from "react-router-dom";
import classes from "./Header.module.css";
import AuthorizeApi from '../../api/authorizeApi';
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import userApi from '../../api/UserApi';
import jwt_decode from "jwt-decode";
import AccountMenu from "./AccountMenu";
import { LeftDrawer } from "./LeftDrawer";
import { ClassNames } from "@emotion/react";
import { List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import DiningIcon from '@mui/icons-material/Dining';
import LocalGroceryStoreIcon from '@mui/icons-material/LocalGroceryStore';
import NewspaperIcon from '@mui/icons-material/Newspaper';
import AddLocationAltIcon from '@mui/icons-material/AddLocationAlt';
import ModelTrainingIcon from '@mui/icons-material/ModelTraining';
import { getByUserNutrition } from "../../api/nutritionsApi";

import {
  HomeFilled,
} from '@ant-design/icons';

let authService = new AuthorizeApi();

const HeaderContainer = () => {
  const user = AuthorizeApi.isSignedIn();
  const [name, setName] = useState<string>();
  const [programLink, setProgram] = useState<string>("");
  const [nutritionLink, setNutrition] = useState<string>("");
  const [id, setId] = useState<string>("");
  const token = AuthStore.getToken() as string;
  const signedIn = AuthorizeApi.isSignedIn();
  const [userState, setUserState] = useState(signedIn);
  const [userRole, setUser] = useState<string[]>();
  const [canEdit, setCanEdit] = useState(false);

  const fetchData = async () => {
    if (user) {
      // debugger
      const user: any = jwt(token);
      let decodedJwt = jwt_decode(token) as any;
      let roles = decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] as string[];
      setUser(roles);
      setCanEdit(roles.includes("Admin"));
      await userApi.getById(user.nameid).then(async response => {
        setName(response.data.name);
        setProgram(typeof response.data.trainingProgramId == "undefined" ? "/training-program" : `/training-program/get/${response.data.trainingProgramId}`)
        if (name !== undefined) {
          setUserState(true);
        }
        setId(response.data.id);

      })
      await getByUserNutrition(user.nameid).then(r => setNutrition(typeof r.data.id == "undefined" ? "/nutritions" : `/nutritions/get/${r.data.id}`));
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const onLogoutClick = async () => {
    await authService.logout();
    setUserState(false);
  }
  
  return (
    <Layout.Header className={classes.headerContainer}>
      {signedIn && userState ?(<>
      <div className="headerMenu" style={{display: "flex"}}>
        <LeftDrawer>
            <List>
            <NavLink to="/" className={classes.menuTextHomeItem}>
              <ListItem button key="1">
              <HomeFilled />
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="Home"/>
              </ListItem>
              </NavLink>
            <NavLink to="/places/" className={classes.menuText}>
              <ListItem button key="1">
              <AddLocationAltIcon />
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="Places"/>
              </ListItem>
              </NavLink>
            <NavLink to="/training-program" className={classes.menuText}>
              <ListItem button key="2">
              <ModelTrainingIcon />
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="Training programs"/>
              </ListItem>
              </NavLink>
            <NavLink to="/nutritions" className={classes.menuText}>
              <ListItem button key="3">
              <DiningIcon />
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="Nutritions"/>
              </ListItem>
              </NavLink>
            <NavLink to="/shops" className={classes.menuText}>
              <ListItem button key="4">
              <LocalGroceryStoreIcon/>
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="Shops"/>
              </ListItem>
              </NavLink>
            <NavLink to="/news" className={classes.menuText}>
              <ListItem button key="5">
              <NewspaperIcon />
                <ListItemIcon>
                  {/* {index % 2 === 0 ? <InboxIcon /> : <MailIcon />} */}
                </ListItemIcon>
                <ListItemText primary="News"/>
              </ListItem>
              </NavLink>
          </List>
        </LeftDrawer>
        <Menu mode="horizontal" className={classes.headerMenu}>
        <Menu.Item className={classes.headerItem} key="1">
          <div className={classes.headerLogo}>
            <NavLink to="/" style={{color:"black"}}>
              <p className={classes.LogoName} style={{color:"black"}}>
                Sports Hub
                </p>
            </NavLink>
          </div>
        </Menu.Item>
        </Menu></div>
      </>):
      <Menu mode="horizontal" className={classes.headerMenu}>
      <Menu.Item className={classes.headerItem} key="1">
        <div className={classes.headerLogo}>
          <NavLink to="/">
            <p className={classes.LogoName}>
              Sports Hub
              </p>
          </NavLink>
        </div>
      </Menu.Item>
      </Menu>
    }
      {signedIn && userState ? (
        <>
          <AccountMenu onLogoutClick = {onLogoutClick} userName = {name} programLink={programLink} nutritionLink={nutritionLink}/>
        </>
      ) : (
          <Menu mode="horizontal" className={classes.headerMenu}>
            <Menu.Item className={classes.headerItem} key="3">
              <NavLink
                to="/signin"
                className={classes.headerLink}
              >
                Log In
              </NavLink>
            </Menu.Item>
            <Menu.Item className={classes.headerItem} key="4">
              <NavLink
                to="/signup"
                className={classes.headerLink}
              >
                Sign Up
              </NavLink>
            </Menu.Item>
          </Menu>
        )}
    </Layout.Header>
  );
};
export default HeaderContainer;
