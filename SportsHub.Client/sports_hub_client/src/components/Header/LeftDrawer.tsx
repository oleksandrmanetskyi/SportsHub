import * as React from 'react';
// import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import { Drawer, Box, Typography, IconButton } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu'
import { useState } from 'react'

// import { Typogr aphy } from 'antd';

type BoxProps = {
  children: React.ReactNode; // 👈️ type children
};

// const LeftDrawer = (props: BoxProps) => {
//   return ;
// };


export const LeftDrawer = (props: BoxProps) => {
  const [isDrawerOpen,  setIsDrawerOpen] = useState(false)
  return (
    <>
    <IconButton 
    size='large'
    edge='start'
    color='inherit'
    aria-label='logo'
    onClick={() => setIsDrawerOpen(true)}
    > <MenuIcon/>
    </IconButton>
      <Drawer
      anchor='left'
      open={isDrawerOpen}
      onClose={() => setIsDrawerOpen(false)}
      >
        <Box p={2} width='250px' textAlign='left' role='presentation'>
          <Typography variant='h6' component='div'>
            {/* Menu */}
            <div>{props.children}</div>
          </Typography>
        </Box>
      </Drawer>
    </>
  )
}




