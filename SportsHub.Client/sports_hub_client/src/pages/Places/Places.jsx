import React, { useEffect, useState } from 'react';
import Map from "./google/Map";
import Marker from "./google/Marker";
import TransitLayer from "./google/TransitLayer";
import { getPlaces } from "../../api/placesApi";
import Place from "../../models/Place";
import jwt from 'jwt-decode';
import AuthStore from '../../stores/AuthStore';
import "./Places.css"
import SplitButton from "./SplitButton"
import ChangePlaceForm from "./ChangePlaceForm"
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';

export default function Places() {
  const [places, setPlaces] = useState([]);
  const [placeIndex, setPlaceIndex] = useState(0);
  const [bound, setBound] = useState({});
  const [transitLayerEnabled, setTransitLayerEnabled] = useState(false);
  const [reload, setReload] = useState(false);

  const fetchData = async () => {
    const token = AuthStore.getToken();
    const user = jwt(token);
    let response = await getPlaces(user.nameid);
    setPlaces(response.data.results);
  };

  useEffect(() => {
    fetchData();
  }, [reload]);

  return (<>{places.length==0 ? null : 
    <div className="mapMain">
      {/* <Container>
        <Box sx={{ bgcolor: '#dde1e5', marginTop: "70px", paddingTop: "3px" }} > */}
        <h2 className='header'> Places to do your sport </h2>
      <div className="mapWithButtons">
      <Map
        zoom={12}
        center={{ lat: places[placeIndex].geometry.location.lat, lng: places[placeIndex].geometry.location.lng }}
        events={{ onBoundsChangerd: arg => setBound(arg) }}
      >
        <TransitLayer enabled={transitLayerEnabled} />
        {places.map((m, index) => (
          <Marker
            key={m.place_id}
            active={placeIndex === index}
            title={m.name}
            position={{ lat: m.geometry.location.lat, lng: m.geometry.location.lng }}
            events={{
              onClick: () => window.alert(`marker ${index} clicked`)
            }}
          />
        ))}
      </Map>
      {/* <SplitButton onNextPlaceClick = {setPlaceIndex((placeIndex + 1) % places.length)} onToggleClick = {setTransitLayerEnabled(!transitLayerEnabled)}/> */}
      <div className="buttons">
        <div className="but">
          <ChangePlaceForm fetchData = {fetchData}/>
        </div>
        <div className="but">
          <SplitButton onNextPlaceClick = {() => setPlaceIndex((placeIndex + 1) % places.length)} onToggleClick = {() => setTransitLayerEnabled(!transitLayerEnabled)}/>
        </div>
        
      {/* <button
        className="btn"
        onClick={() => setPlaceIndex((placeIndex + 1) % places.length)}
      >
        Next place
      </button>
      <br />
      <button
        className="btn"
        onClick={() => setTransitLayerEnabled(!transitLayerEnabled)}
      >
        Toggle transit layer
      </button> */}
      </div>
      </div>
      {/* <br />
      Current place id: {places[placeIndex].place_id}
      <br />
      Map bounds: {bound.toString()} */}

        {/* </Box>
      </Container> */}
      
    </div>}</>
  );
}
