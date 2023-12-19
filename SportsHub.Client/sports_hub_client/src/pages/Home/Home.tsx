import React from "react";
import { Layout, Carousel, Card } from "antd";
import classes from "./Home.module.css";


const Home = () => {
  return (
    <Layout.Content>
      <div className={classes.cards__container}>
        <Card bordered={false} className={classes.cardsBack}>
          <div className={classes.card__body}>
            <div className={classes.card__title}>
              <p>What is Sports Hub?</p>
              <div className={classes.cardText}>
              <p className={classes.text}>
              Sports Hub is an application to make more easier to do sport. All you need it is sing up or log in and take pleasure of using it.
              </p>
            </div>
            </div>
            
          </div>
        </Card>
        
      </div>
    </Layout.Content>
  );

};
export default Home;
