import * as React from 'react';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import PersonAdd from '@mui/icons-material/PersonAdd';
import Settings from '@mui/icons-material/Settings';
import Logout from '@mui/icons-material/Logout';
import { NavLink } from 'react-router-dom';
import { EditOutlined } from '@mui/icons-material';
import classes from "./Header.module.css";
import ModelTrainingTwoToneIcon from '@mui/icons-material/ModelTrainingTwoTone';
import DiningIcon from '@mui/icons-material/Dining';

interface Props {
    onLogoutClick: () => Promise<void>;
    userName: string | undefined;
    programLink: string;
    nutritionLink: string;
  }

export default function AccountMenu(props: Props) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  return (
    <React.Fragment>
      <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
        <Tooltip title="Account">
          <IconButton className={classes.accountIconButton}
            onClick={handleClick}
            size="small"
            sx={{ ml: 2 }}
            aria-controls={open ? 'account-menu' : undefined}
            aria-haspopup="true"
            aria-expanded={open ? 'true' : undefined}
          >
            <Avatar sx={{ width: 42, height: 42, color:'black' }}>{props.userName?.charAt(0).toUpperCase()}</Avatar>
            
          </IconButton>
        </Tooltip>
      </Box>
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        PaperProps={{
          elevation: 0,
          sx: {
            overflow: 'visible',
            filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
            mt: 1.5,
            '& .MuiAvatar-root': {
              width: 32,
              height: 32,
              ml: -0.5,
              mr: 1,
            },
            '&:before': {
              content: '""',
              display: 'block',
              position: 'absolute',
              top: 0,
              right: 14,
              width: 10,
              height: 10,
              bgcolor: 'background.paper',
              transform: 'translateY(-50%) rotate(45deg)',
              zIndex: 0,
            },
          },
        }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        <NavLink to="/profile" className={classes.headerLink} >
        <MenuItem>          
          <Avatar /> Profile
        </MenuItem>
        </NavLink>
        <NavLink to={props.programLink} className={classes.headerLink} >
        <MenuItem>          
          <ListItemIcon>
            <ModelTrainingTwoToneIcon fontSize="medium" />
          </ListItemIcon>
          Training program
        </MenuItem>
        </NavLink>
        <NavLink to={props.nutritionLink} className={classes.headerLink} >
        <MenuItem>    
          <ListItemIcon>
            <DiningIcon fontSize="medium" />
          </ListItemIcon>      
          Nutrition
        </MenuItem>
        </NavLink>
        <Divider />
        <NavLink
          className={classes.headerLink}
          to="/signin"
          onClick={props.onLogoutClick}
        >
        <MenuItem>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          Logout
        </MenuItem>
        </NavLink>
      </Menu>
    </React.Fragment>
  );
}