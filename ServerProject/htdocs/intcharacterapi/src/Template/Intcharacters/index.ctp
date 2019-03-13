<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Intcharacter[]|\Cake\Collection\CollectionInterface $intcharacters
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('New Intcharacter'), ['action' => 'add']) ?></li>
    </ul>
</nav>
<div class="intcharacters index large-9 medium-8 columns content">
    <h3><?= __('Intcharacters') ?></h3>
    <table cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th scope="col"><?= $this->Paginator->sort('Id') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Name') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Speed') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Jump') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Hp') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Power') ?></th>
                <th scope="col"><?= $this->Paginator->sort('Item') ?></th>
                <th scope="col" class="actions"><?= __('Actions') ?></th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($intcharacters as $intcharacter): ?>
            <tr>
                <td><?= $this->Number->format($intcharacter->Id) ?></td>
                <td><?= h($intcharacter->Name) ?></td>
                <td><?= $this->Number->format($intcharacter->Speed) ?></td>
                <td><?= $this->Number->format($intcharacter->Jump) ?></td>
                <td><?= $this->Number->format($intcharacter->Hp) ?></td>
                <td><?= $this->Number->format($intcharacter->Power) ?></td>
                <td><?= $this->Number->format($intcharacter->Item) ?></td>
                <td class="actions">
                    <?= $this->Html->link(__('View'), ['action' => 'view', $intcharacter->Id]) ?>
                    <?= $this->Html->link(__('Edit'), ['action' => 'edit', $intcharacter->Id]) ?>
                    <?= $this->Form->postLink(__('Delete'), ['action' => 'delete', $intcharacter->Id], ['confirm' => __('Are you sure you want to delete # {0}?', $intcharacter->Id)]) ?>
                </td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
    <div class="paginator">
        <ul class="pagination">
            <?= $this->Paginator->first('<< ' . __('first')) ?>
            <?= $this->Paginator->prev('< ' . __('previous')) ?>
            <?= $this->Paginator->numbers() ?>
            <?= $this->Paginator->next(__('next') . ' >') ?>
            <?= $this->Paginator->last(__('last') . ' >>') ?>
        </ul>
        <p><?= $this->Paginator->counter(['format' => __('Page {{page}} of {{pages}}, showing {{current}} record(s) out of {{count}} total')]) ?></p>
    </div>
</div>
