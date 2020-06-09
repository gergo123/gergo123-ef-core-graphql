import React, { } from 'react';
import './App.css';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';

const step0 = (props) => {
	return (
		<>
			<p>Step zero, starting the flow.</p>
			<TextField
				id="standard-name"
				label="Name"
				onChange={props.handleChange('name')}
				margin="normal"
			/>
		</>
	);
};

const step1 = () => {
	return (
		<>
			<p>First state change, nothing really happens.</p>
		</>
	);
};

const step2 = () => {
	return (
		<>
			<p>State changes again, tasks being spawned after finishing this step.</p>
		</>
	);
};

const step3 = () => {
	return (
		<>
			<p>Step requires every tasks previously made to be finished (you can finish a task by clicking on the 'Task x' label).</p>
		</>
	);
};

const step4 = (props) => {
	const onCheckboxChanged = (event) => {
		props.handleChange('FourToFiveVM')({
			target: {
				value: {
					...props.values.FourToFiveVM,
					FailChange: event.target.checked
				}
			}
		});
	};

	return (
		<>
			<p>Everything got approved, we went through.</p>
			<FormControlLabel
				value="start"
				control={<Checkbox checked={props.values.FourToFiveVM.FailChange} onChange={onCheckboxChanged} color="primary" />}
				label="Fail on isValid check?"
				labelPlacement="start"
			/>
		</>
	);
};

const step5 = () => {
	return (
		<>
			<p>Last step, not much going on here.</p>
		</>
	);
};

export { step0, step1, step2, step3, step4, step5 };
