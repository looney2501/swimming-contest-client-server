import React from 'react';
//import ReactDOM from 'react-dom';
import {createRoot} from 'react-dom/client';
import './index.css';
import RaceApp from "./RaceApp";

const container = document.getElementById('root');
const root = createRoot(container);
root.render(<RaceApp/>);

/*ReactDOM.render(
  <div>
 <RaceApp/>
  </div>,
  document.getElementById('root')
);*/
