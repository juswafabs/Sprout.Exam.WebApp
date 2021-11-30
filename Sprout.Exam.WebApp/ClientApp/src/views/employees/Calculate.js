import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeCalculate extends Component {
  static displayName = EmployeeCalculate.name;
   
  constructor(props) {
    super(props);
    this.state = { id: 0,fullName: '',birthdate: '',tin: '',typeId: 1,absentDays: 0.00,workedDays: 0.00,netIncome: 0.00, loading: true,loadingCalculate:false };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      this.calculateSalary();
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-12'>
  <label>Full Name: <b>{this.state.fullName}</b></label>
</div>

</div>

<div className='form-row'>
<div className='form-group col-md-12'>
  <label >Birthdate: <b>{this.state.birthdate}</b></label>
</div>
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>TIN: <b>{this.state.tin}</b></label>
</div>
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>Employee Type: <b>{this.state.typeId === 1?"Regular": "Contractual"}</b></label>
</div>
</div>

{ this.state.typeId === 1?
 <div className="form-row">
     <div className='form-group col-md-12'><label>Salary: 20000 </label></div>
     <div className='form-group col-md-12'><label>Tax: 12% </label></div>
</div> : <div className="form-row">
<div className='form-group col-md-12'><label>Rate Per Day: 500 </label></div>
</div> }

<div className="form-row">

{ this.state.typeId === 1? 
<div className='form-group col-md-6'>
  <label htmlFor='inputAbsentDays4'>Absent Days: </label>
  <input type='text' className='form-control' id='inputAbsentDays4' onChange={this.handleChange.bind(this)} value={this.state.absentDays} name="absentDays" placeholder='Absent Days' />
</div> :
<div className='form-group col-md-6'>
  <label htmlFor='inputWorkDays4'>Worked Days: </label>
  <input type='text' className='form-control' id='inputWorkDays4' onChange={this.handleChange.bind(this)} value={this.state.workedDays} name="workedDays" placeholder='Worked Days' />
</div>
}
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>Net Income: <b>{this.state.netIncome}</b></label>
</div>
</div>

<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingCalculate} className="btn btn-primary mr-2">{this.state.loadingCalculate?"Loading...": "Calculate"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Calculate Salary</h1>
        <br/>
        {contents}
      </div>
    );
  }

  async calculateSalary() {

    if (isNaN(this.state.absentDays) || isNaN(this.state.workedDays)) {
      alert("Please input a number.");
      return;
    }

    if (this.state.absentDays < 0 || this.state.workedDays < 0) {
      alert("Cannot input number below zero.");
      return;
    }

    this.setState({ loadingCalculate: true });

    try {
              const token = await authService.getAccessToken();
              const requestOptions = {
                  method: 'POST',
                  headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
                  body: JSON.stringify({id: this.state.id, absentDays: parseFloat(this.state.absentDays), workedDays: this.state.workedDays})
              };
          
              const response = await fetch('api/employees/calculate', requestOptions);
              const data = await response.json();
              if(data.isSuccessful){
                this.setState({ loadingCalculate: false, netIncome: (data.data).toFixed(2) });
              }
              else{
                this.setState({ loadingCalculate: false });
                alert(data.error);
              }
        } catch (error) {
              this.setState({ loadingCalculate: false });
              alert("An error has occured. Please contact support.")
      }
  }

  async formatDate(data) {
    var dt = new Date(data).toLocaleDateString('en-CA');

    return dt;
  }

  async getEmployee(id) {
    this.setState({ loading: true, loadingSave: false });

    try {
          const token = await authService.getAccessToken();
          const response = await fetch('api/employees/' + id, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
          });
          const data = await response.json();
      
          if(data.isSuccessful){
            let employeeData = data.data;
            var bdate = await this.formatDate(employeeData.birthdate);
      
            this.setState({ id: employeeData.id, fullName: employeeData.fullName, birthdate: bdate, tin: employeeData.tin, typeId: employeeData.employeeTypeId, loading: false,loadingSave: false });
          }
          else{
              alert(data.error);
          }
    } catch (error) {
          this.setState({ loading: false, loadingSave: false });
          alert("An error has occured. Please contact support.")
    }

   

  }

}
