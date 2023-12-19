import React, { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Layout } from "antd";
import AuthStore from '../../stores/AuthStore';
import { Container, Box } from "@mui/material";

const { Content } = Layout;

const PrivateLayout = ({ children }: any) => {
  const history = useHistory();

  const fetchData = async () => {
    const token = AuthStore.getToken() as string;
    if (token == null) {
      history.push("/signin");
    }
  };


  useEffect(() => {
    fetchData();
  }, []);

  return (
    <Container>
      <Box sx={{ bgcolor: '#dde1e5', marginTop: "70px", paddingTop: "10px" , paddingBottom: "7px", marginBottom:"15px"}} >
        <div
          // className="site-layout-background"
          // style={{ padding: 20, minHeight: 360 }}
        >
          {children}
        </div>
        </Box>
    </Container>
  );
};

export default PrivateLayout;