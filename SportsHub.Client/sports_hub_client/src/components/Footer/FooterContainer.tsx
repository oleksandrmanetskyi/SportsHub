import React from "react";
import { Layout } from "antd";
import classes from "./Footer.module.css";

const FooterContainer = () => {
  return (
    <Layout.Footer className={classes.footerContainer}>
      <div className={classes.footerTitle}>
        <p>Sports Hub</p>
      </div>
      <p className={classes.footerCopyright}>© 2023, Sports Hub Ukraine. All rights reserved.</p>
    </Layout.Footer>
  );
};
export default FooterContainer;
