import React from 'react';
import RaceTable from './RaceTable';
import './RaceApp.css'
import RaceFormAdd from "./RaceFormAdd";
import {AddRace, DeleteRace, GetRaces, ModifyRace} from './utils/rest-calls'
import RaceFormModify from "./RaceFormModify";


class RaceApp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            races: [],
            deleteFunc: this.deleteFunc.bind(this),
            addFunc: this.addFunc.bind(this),
            modifyFunc: this.modifyFunc.bind(this),
            form: new RaceFormAdd
        }
        console.log('RaceApp constructor')
    }

    addFunc(race) {
        console.log('inside add Func ' + race);
        AddRace(race)
            .then(() => GetRaces())
            .then(races => this.setState({races: races}))
            .catch(erorr => console.log('eroare add ', erorr));
    }


    deleteFunc(race) {
        console.log('inside deleteFunc ' + race);
        DeleteRace(race)
            .then(() => GetRaces())
            .then(races => this.setState({races}))
            .catch(error => console.log('eroare delete', error));
    }

    modifyFunc(race) {
        console.log('inside modifyFunc ' + race);
        ModifyRace(race)
            .then(() => GetRaces())
            .then(races => this.setState({races}))
            .catch(error => console.log('eroare modify', error));
    }

    handleGetRaceData(race) {
        this.setState({race: race})
    }

    componentDidMount() {
        console.log('inside componentDidMount')
        GetRaces().then(races => this.setState({races: races}));
    }

    render() {
        return (
            <div className="RaceApp">
                <h1>Chat User Management</h1>
                <div id={'formsDiv'}>
                    <RaceFormAdd addFunc={this.state.addFunc}/>
                    <RaceFormModify modifyFunc={this.state.modifyFunc}></RaceFormModify>
                </div>
                <br/>
                <br/>
                <RaceTable races={this.state.races} deleteFunc={this.state.deleteFunc}/>
            </div>
        );
    }
}

export default RaceApp;