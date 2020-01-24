import React, { useState, useEffect } from 'react';
import './App.css';
import Button from '@material-ui/core/Button';
import Box from '@material-ui/core/Box';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import StepContent from '@material-ui/core/StepContent';
import TextField from '@material-ui/core/TextField';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControlLabel from '@material-ui/core/FormControlLabel';

import TaskList from './Tasks';
import { step0, step1, step2, step3, step4, step5 } from './Steps';

const App = (props) => {
	// TODO error flag in steps array
	// TODO INotification interface server side e-mail küldésre
	// done Change objektum lenne megiscsak, azon entitas idk
	// change tipusok entitasok lennenek talan
	// -> nem kell annak lenni mert egyedi fejlesztes, nem feluleten kell dinamikusan tarolni
	const APIURL = 'http://localhost:37680/';
	const [errorMessage, setErrorMessage] = useState('');
	const [popupOpen, setPopupOpen] = useState(false);

	const [tasks, setTasks] = useState([]);
	const [activeStep, setActiveStep] = useState(0);
	const [values, setValues] = React.useState({
		processId: 0,
		message: '',

		FourToFiveVM: {
			FailChange: false
		}
	});

	useEffect(() => {
		var r = /\d+/;
		var s = window.location.search;
		var id = s.match(r);

		if (id && id > 0) {
			getEntityById(id).then((entity) => {
				setValues({
					...values,
					processId: entity.id,
					message: entity.message,
				});
				setActiveStep(entity.currentState + 1);
				getTasks(entity.id);
			});
		}
	}, []);

	const onHandleChange = name => event => {
		setValues({ ...values, [name]: event.target.value });
	};
	const nextStep = () => {
		return steps[activeStep].action().then(() => {
			setActiveStep(activeStep + 1);
			// Refresh task list
			getTasks();
		});
	};

	const getTasks = (id = 0) => {
		var urlParam = values.processId;
		if (id > 0) {
			urlParam = id;
		}
		fetch(`${APIURL}api/flowone/GetSpecificTasks?entityId=${urlParam}`, {
			credentials: 'include',
		}).then((resp) => {
			if (resp.ok) {
				return resp.json().then((json) => {
					console.log(json);
					setTasks(json.tasks);
					return Promise.resolve();
				});
			} else {
				return Promise.reject(resp);
			}
		}).catch(error);
	};

	const getEntityById = (id) => {
		return fetch(`${APIURL}api/FlowOne/GetById?entityId=${id}`, {
			credentials: 'include',
		}).then((resp) => {
			if (resp.ok) {
				return resp.json().then((json) => {
					return Promise.resolve(json.entity);
				});
			} else {
				return Promise.reject(resp);
			}
		}).catch(error);
	};

	const postStepData = (stepId, body = {}) => {
		return fetch(APIURL + `api/flowone/${stepId}?entityId=${values.processId}`, {
			mode: 'cors',
			method: 'post',
			credentials: 'include',
			body: JSON.stringify(body),
			headers: {
				'Content-Type': 'application/json'
			}
		})
			.then((resp) => {
				// Assuming receiving json response from server even on error
				return resp.json().then((json) => {
					if (!resp.ok) { return Promise.reject(json); } else { return Promise.resolve(json) }
				});
			})
			.catch(error);
	};
	const error = (message) => {
		console.log(message);
		setErrorMessage(message.message);
		setPopupOpen(true);
		return Promise.reject(message);
	};
	const step0Action = () => {
		const { message } = values;
		return postStepData('Create', { message }).then((resp) => {
			onHandleChange('processId')({ target: { value: resp.id } });
			return Promise.resolve();
		});
	};
	const step1Action = () => {
		return postStepData('StepOne');
	};
	const step2Action = () => {
		return postStepData('StepTwo', {}, true).then((resp) => console.log(resp.ids));
	};
	const step3Action = () => {
		return postStepData('StepThree').catch((message) => {
			//alert(message.message);
			return Promise.reject();
		});
	};
	const step4Action = () => {
		return postStepData('StepFour', values.FourToFiveVM);
	};
	const step5Action = () => {
		//return postStepData('StepFive');
	};
	const handleClose = () => {
		setPopupOpen(false);
	};

	const steps = [
		{ label: 'Step zero', comp: step0, action: step0Action, hasError: false }, { label: 'Step One', comp: step1, action: step1Action, hasError: false },
		{ label: 'Step Two', comp: step2, action: step2Action, hasError: false }, { label: 'Step Three', comp: step3, action: step3Action, hasError: false },
		{ label: 'Step Four', comp: step4, action: step4Action, hasError: false }, { label: 'Step Five', comp: step5, action: step5Action, hasError: false }];

	return (
		<Box style={{ height: '100%', width: '100%' }} display="flex" justifyContent="center" alignItems="center" flexDirection="column">
			<Box style={{ width: '50%' }} display="flex" flexDirection="row">
				<div>
					<TaskList tasks={tasks} error={error} getTasks={getTasks} apiUrl={APIURL}></TaskList>
				</div>

				<Stepper style={{ width: '100%' }} activeStep={activeStep} orientation="vertical">
					{steps.map((item, index) => {
						return <Step key={item.label}>
							<StepLabel error={item.hasError}>{item.label}</StepLabel>
							<StepContent>
								<item.comp handleChange={onHandleChange} values={values}></item.comp>
							</StepContent>
						</Step>
					})}
				</Stepper>

			</Box>

			<Button variant="contained" color="primary" onClick={nextStep} disabled={!steps[activeStep + 1]}>
				Next step
			</Button>

			<Dialog
				open={popupOpen}
				onClose={handleClose}
				aria-labelledby="alert-dialog-title"
				aria-describedby="alert-dialog-description"
			>
				<DialogTitle id="alert-dialog-title">{"Hiba történt"}</DialogTitle>
				<DialogContent>
					<DialogContentText id="alert-dialog-description">
						{errorMessage}
					</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button onClick={handleClose} color="primary" autoFocus>
						Ok
					</Button>
				</DialogActions>
			</Dialog>
		</Box>
	)
};

export default App;
