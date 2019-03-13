<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Intcharacter $intcharacter
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('Edit Intcharacter'), ['action' => 'edit', $intcharacter->Id]) ?> </li>
        <li><?= $this->Form->postLink(__('Delete Intcharacter'), ['action' => 'delete', $intcharacter->Id], ['confirm' => __('Are you sure you want to delete # {0}?', $intcharacter->Id)]) ?> </li>
        <li><?= $this->Html->link(__('List Intcharacters'), ['action' => 'index']) ?> </li>
        <li><?= $this->Html->link(__('New Intcharacter'), ['action' => 'add']) ?> </li>
    </ul>
</nav>
<div class="intcharacters view large-9 medium-8 columns content">
    <h3><?= h($intcharacter->Id) ?></h3>
    <table class="vertical-table">
        <tr>
            <th scope="row"><?= __('Name') ?></th>
            <td><?= h($intcharacter->Name) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Id') ?></th>
            <td><?= $this->Number->format($intcharacter->Id) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Speed') ?></th>
            <td><?= $this->Number->format($intcharacter->Speed) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Jump') ?></th>
            <td><?= $this->Number->format($intcharacter->Jump) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Hp') ?></th>
            <td><?= $this->Number->format($intcharacter->Hp) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Power') ?></th>
            <td><?= $this->Number->format($intcharacter->Power) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Item') ?></th>
            <td><?= $this->Number->format($intcharacter->Item) ?></td>
        </tr>
    </table>
</div>
